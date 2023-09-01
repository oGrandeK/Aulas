using System.Text.RegularExpressions;
using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Context.SharedContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Extensions;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    public string Address { get; }
    public string Hash => Address.ToBase64();
    public Verification Verification { get; private set; } = new Verification();

    [GeneratedRegex(Pattern)]
    private static Regex EmailRegex() => new Regex(Pattern);

    protected Email() {
        
    }

    public Email(string address)
    {
        if(string.IsNullOrEmpty(address)) throw new Exception("E-Mail inválido");

        Address = address.Trim().ToLower();

        if(Address.Length < 5) throw new Exception("E-Mail inválido");

        if(!EmailRegex().IsMatch(Address)) throw new Exception("E-Mail inválido");
    }

    public static implicit operator string(Email email) => email.ToString();
    public static implicit operator Email(string address) => new Email(address);
    public override string ToString() => Address;

    public void ResendVerification() {
        Verification = new Verification();
    }
}

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
internal class GeneratedRegexAttribute : Attribute
{
    public string Pattern { get; }

    public GeneratedRegexAttribute(string pattern) {
        Pattern = pattern;
    }
}