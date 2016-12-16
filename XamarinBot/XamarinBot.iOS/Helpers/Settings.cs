using Microsoft.Bot.Connector.DirectLine;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinBot.iOS
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string ConvoKey = "convo_key";
        private static readonly Conversation ConvoDefault = null;

        private const string WatermarkKey = "watermark_key";
        private static readonly string WatermarkDefault = string.Empty;

        public static object Convo
        {
            get
            {
                return AppSettings.GetValueOrDefault<object>(ConvoKey, ConvoDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<object>(ConvoKey, value);
            }
        }

        public static string Watermark
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(WatermarkKey, WatermarkDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(WatermarkKey, value);
            }
        }

    }
}
