using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafe.Http.Net
{
	//Based on my FreecraftCore.Serializer: https://github.com/FreecraftCore/FreecraftCore.Serializer/blob/152fda27c46fcfcaa72d3568c1a591d728773f33/src/FreecraftCore.Serializer.API/Reflection/Serialization/MemberSerializationMediator.cs
	public sealed class MemberReflectionTypeMediator<TContainingType> : MemberReflectionTypeMediator
	{
		/// <summary>
		/// Delegate that can grab the <see cref="MemberInformation"/> member value.
		/// </summary>
		private Func<TContainingType, object> MemberGetter { get; }

		public MemberReflectionTypeMediator(MemberInfo memberInfo)
		{
			if (memberInfo == null) throw new ArgumentNullException(nameof(memberInfo));

			//Due to perf problems fasterflect setting wasn't fast enough.
			//Introducing a compiled lambda to delegate for get/set should provide the much needed preformance.

			//Build the getter lambda
			MemberGetter = BuildMemberGetter(memberInfo);
		}

		private Func<TContainingType, object> BuildMemberGetter(MemberInfo memberInfo)
		{
			try
			{
				ParameterExpression instanceOfTypeToReadMemberOn = Expression.Parameter(memberInfo.DeclaringType, "instance");
				MemberExpression member = GetPropertyOrFieldExpression(instanceOfTypeToReadMemberOn, memberInfo.Name, memberInfo);
				UnaryExpression castExpression = Expression.TypeAs(member, typeof(object)); //use object to box

				//Build the getter lambda
				return Expression.Lambda(castExpression, instanceOfTypeToReadMemberOn).Compile()
					as Func<TContainingType, object>;
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Failed to prepare getter for {typeof(TContainingType).FullName}'s with member Name: {memberInfo.Name}.", e);
			}
		}

		private static FieldInfo GetFieldInfo(Type type, string name)
		{
			FieldInfo[] fields = type.GetRuntimeFields()
				.Where(p => p.Name.Equals(name))
				.ToArray();

			if (fields.Length == 1)
			{
				//Core of the fix: if the type is not the same as the type who declared the property we should look at the declaring type
				return fields[0].DeclaringType == type ? fields[0]
					:
					fields[0].DeclaringType.GetRuntimeFields()
						.FirstOrDefault(p => p.Name.Equals(name));
			}
			else
			{
				throw new NotSupportedException(name);
			}
		}

		private static PropertyInfo GetPropertyInfo(Type type, string name)
		{
			PropertyInfo[] properties = type.GetRuntimeProperties()
				.Where(p => p.Name.Equals(name))
				.ToArray();

			if (properties.Length == 1)
			{
				//Core of the fix: if the type is not the same as the type who declared the property we should look at the declaring type
				return properties[0].DeclaringType == type ? properties[0]
					: properties[0].DeclaringType.GetRuntimeProperties()
						.FirstOrDefault(p => p.Name.Equals(name));
			}
			else
			{
				throw new NotSupportedException(name);
			}
		}

		//TODO: Figure out why we have to do this in later versions of .NET/netstandard
		//We have to use this hack to handle properties from inherited classes
		//See: http://stackoverflow.com/a/8042602
		private static MemberExpression GetPropertyOrFieldExpression(Expression baseExpr, string name, MemberInfo info)
		{
			if (baseExpr == null) throw new ArgumentNullException(nameof(baseExpr));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));

			Type type = baseExpr.Type;

			//TODO: Refactor
			if (info is PropertyInfo)
			{
				return Expression.Property(baseExpr, GetPropertyInfo(type, name));
			}
			else if (info is FieldInfo)
			{
				return Expression.Field(baseExpr, GetFieldInfo(type, name));
			}

			throw new NotSupportedException($"Provided member Name: {name} is neither a field nor a property.");
		}

		/// <inheritdoc />
		public override object Access(object from)
		{
			if (from == null) throw new ArgumentNullException(nameof(from));

			return MemberGetter((TContainingType)from);
		}
	}

	public abstract class MemberReflectionTypeMediator
	{
		/// <summary>
		/// Delegate that can grab the <see cref="MemberInformation"/> member value.
		/// </summary>
		public abstract object Access(object from);
	}
}