using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Utilities;

namespace MikroTikMiniApi.Sentences
{
    using ILocalizationService = IApiSentenceLocalizationService;

    ///<inheritdoc cref="IApiSentence"/>
    internal abstract class ApiSentenceBase : IApiSentence, IEquatable<ApiSentenceBase>
    {
        private readonly int _wordsHashCode;
        private readonly int _typeHashCode;
        private readonly IReadOnlyDictionary<string, string> _wordsValues;
        private readonly ILocalizationService _localizationService;

        ///<inheritdoc/>
        public IReadOnlyList<string> Words { get; }

        protected ApiSentenceBase(IReadOnlyList<string> words, ILocalizationService localizationService)
        {
            Guard.ThrowIfNull(words, nameof(words));
            Guard.ThrowIfNull(localizationService, out _localizationService, nameof(localizationService));

            _wordsValues = GetWordsValues(words, out _wordsHashCode);
            _typeHashCode = GetType().GetHashCode();

            Words = words;
        }

        private static IReadOnlyDictionary<string, string> GetWordsValues(IEnumerable<string> words, out int wordsHashCode)
        {
            var wordsValues = new Dictionary<string, string>();
            var regex = new Regex("^=?(?<KEY>[^=]+)=(?<VALUE>.+)$", RegexOptions.Singleline);

            wordsHashCode = 0;

            foreach (var word in words)
            {
                wordsHashCode ^= word.GetHashCode();

                var match = regex.Match(word);

                if (!match.Success)
                    continue;

                var key = match.Groups["KEY"].Value;
                var value = match.Groups["VALUE"].Value;

                wordsValues.Add(key, value);
            }

            return wordsValues;
        }

        internal static string GetTextInternal(IReadOnlyCollection<string> words, ILocalizationService localizationService)
        {
            if (words.Count == 0)
                return localizationService.GetResponseIsEmptyText();

            var sb = new StringBuilder();

            foreach (var word in words)
                sb.Append(word);

            return sb.ToString();
        }

        ///<inheritdoc/>
        public bool TryGetWordValue(string word, out string value)
        {
            Guard.ThrowIfNull(word, nameof(word));

            return _wordsValues.TryGetValue(word, out value!);
        }

        ///<inheritdoc/>
        public string GetText()
        {
            return GetTextInternal(Words, _localizationService);
        }

        public bool Equals(ApiSentenceBase? other)
        {
            return Equals((object?)other);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ApiSentenceBase other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Words.Count != other.Words.Count)
                return false;

            for (var i = 0; i < Words.Count; i++)
            {
                if (!Words[i].Equals(other.Words[i], StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _wordsHashCode ^ _typeHashCode;
        }
    }
}