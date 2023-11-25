using System.Reflection;
using System.Runtime.CompilerServices;

namespace DebugKeyRemap;

/* TODO: This should probably be in a library somewhere by this point */
public static class ReflectionExtensions {
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Type Ref(this TypeInfo info) => info.AsType();
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TypeInfo Rd(this Type @ref) => @ref.GetTypeInfo();
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Type Ginst(this Type @base, params Type[] args) => @base.MakeGenericType(args);


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ConstructorInfo Ctor(this TypeInfo type, params Type[] args) => type.DeclaredConstructors.FindBestOverloadFor(args) ?? throw CreateMissingMemberException(type, MemberTypes.Constructor, ".ctor", args);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static FieldInfo Fld(this TypeInfo type, string name) => type.GetDeclaredField(name) ?? throw CreateMissingMemberException(type, MemberTypes.Field, name);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertyInfo Prop(this TypeInfo type, string name) => type.GetDeclaredProperty(name) ?? throw CreateMissingMemberException(type, MemberTypes.Property, name);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static MethodInfo Mth(this TypeInfo type, string name) => type.GetDeclaredMethod(name) ?? throw CreateMissingMemberException(type, MemberTypes.Method, name);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static MethodInfo Mth(this TypeInfo type, string name, params Type[] args) => type.GetDeclaredMethods(name).FindBestOverloadFor(args) ?? throw CreateMissingMemberException(type, MemberTypes.Method, name, args);
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TypeInfo Type(this TypeInfo type, string name) => type.GetDeclaredNestedType(name) ?? throw CreateMissingMemberException(type, MemberTypes.NestedType, name);

	public static string ILName(this Type @ref) {
		if(@ref.FullName == null) {
			/* TODO! */
			return "!" + @ref.Name;
		}
		string? assemblyName = @ref.Assembly.GetName()?.Name;

		if(assemblyName == null) return @ref.FullName;

		return "[" + assemblyName + "]" + @ref.FullName;
	}

	public static T? FindBestOverloadFor<T>(this IEnumerable<T> candidates, Type[] args) where T : MethodBase {
		T? bestCandidate = null;
		foreach(T candidate in candidates) {
			ParameterInfo[] @params = candidate.GetParameters();
			if(@params.Length < args.Length) continue;
			if(!@params.WithIndex().All(b => {
				   (int index, ParameterInfo param) = b;
				   return index >= args.Length ? param.IsOptional : param.ParameterType == args[index];
			})) continue;
			if(bestCandidate == null)
				bestCandidate = candidate;
			else {
				if(bestCandidate.GetParameters().Length > candidate.GetParameters().Length)
					bestCandidate = candidate;
			}
		}

		return bestCandidate;
	}

	private static Exception CreateMissingMemberException(TypeInfo type, MemberTypes what, string name, Type[]? overloadTypes = null) {
		throw new NotImplementedException();
	}


}
