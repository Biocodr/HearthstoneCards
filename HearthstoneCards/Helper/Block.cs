﻿using Windows.UI.Text;
using Windows.UI.Xaml.Documents;

namespace HearthstoneCards.Helper
{
    public enum SpanType
    {
        Normal,
        Bold,
        Italic
    }

    public class Block
    {
        public string Text { get; set; }
        public SpanType SpanType { get; set; }

        public Block(string text, SpanType spanType)
        {
            Text = text;
            SpanType = spanType;
        }

        public Inline CreateInline()
        {
            switch (SpanType)
            {
                case SpanType.Bold:
                    // TODO fix: replace with bold
                    return new Run { Text = Text, FontWeight = FontWeights.Bold };
                case SpanType.Italic:
                    return new Run { Text = Text, FontStyle = FontStyle.Italic };
                case SpanType.Normal:
                    return new Run { Text = Text, FontWeight = FontWeights.Normal, FontStyle = FontStyle.Normal };
                default:
                    return new Run { Text = Text, FontWeight = FontWeights.Normal, FontStyle = FontStyle.Normal };
            }
        }
    }
}
