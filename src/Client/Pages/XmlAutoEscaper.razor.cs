using BlazorMonaco.Editor;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
using System.Xml;

namespace Obaki.Toolkit.Client.Pages
{
    public partial class XmlAutoEscaper
    {
        [Inject] ISnackbar? Snackbar { get; set; }
        [Inject] ITextEditor<string>? TextEditor { get; set; }
        private StandaloneCodeEditor _editor = default!;
        private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "xml"
            };
        }


        private async Task EditorOnDidInit()
        {
            await Global.SetTheme("vs-dark");
        }
        private void Failed(string message)
        {
            Snackbar!.Add(message, Severity.Error);
        }

        private void Succcess(string message)
        {
            Snackbar!.Add(message, Severity.Success);
        }

        private async Task AutoEscape()
        {
            var input = await _editor.GetValue();
            if (!IsXmlIsEmpty(input))
            {
                var escapedResult = TextEditor!.EscapeXmlString(input);
                await _editor.SetValue(escapedResult);
                Succcess("Special characters escaped.");

            }
        }

        private async Task ValidateXml()
        {
            var input = await _editor.GetValue();
            if (!IsXmlIsEmpty(input))
            {
                XmlDocument xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(input);
                    Succcess("Valid XML.");
                }
                catch (XmlException)
                {
                    Failed("Invalid Xml input. Please check the xml for any unescaped special characters(&,<>,\") or  unclosed tags.");
                }

            }
        }

            private bool IsXmlIsEmpty(string xml)
            {
                if (string.IsNullOrEmpty(xml))
                {
                    Failed("Please enter a valid XML string.");
                    return true;
                }
                return false;
            }
        }
    }