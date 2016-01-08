# HashSharp

usage:
```csharp
using HashSharp;

class Example
{
    static void Main()
    {
        string plainText = "plaintexthere";
        var hasher = new PBKDF2
        {
            PlainText = plainText
        };

        string hashedText = hasher.Run();
        string salt = hasher.Salt;

        hasher.Verify(hashedText, plainText); /// true
    }
}
```