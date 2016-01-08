namespace HashSharp.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHasher
    {
        /// <summary>
        /// Hashed text.
        /// </summary>
        string HashedText { get; set; }

        /// <summary>
        /// Plain text.
        /// </summary>
        string PlainText { get; set; }

        /// <summary>
        /// Apply the hash method on <see cref="PlainText"/>.
        /// </summary>
        /// <returns></returns>
        string Run();

        /// <summary>
        /// Indicate whether <paramref name="source"/> and <paramref name="expected"/> are equal.
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        bool Verify(string expected, string source);
    }
}
