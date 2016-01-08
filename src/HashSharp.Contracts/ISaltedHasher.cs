namespace HashSharp.Contracts
{
    public interface ISaltedHasher : IHasher
    {
        string Salt { get; set; }

        bool NeedRehash(string oldSalt);
    }
}
