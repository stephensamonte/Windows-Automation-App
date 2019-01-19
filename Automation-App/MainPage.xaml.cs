using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Automation_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Hello, Jedi Master!");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

            await Task.Delay(5000);
            // MoveMouse();
            TypeText("HelloJedi"); 
            //String currentDate = GetDate(); 
            //TypeText(currentDate);
        }

        // move the mouse to a location on the screen 
        private void MoveMouse()
        {
            var info = new InjectedInputMouseInfo();
            info.MouseOptions = InjectedInputMouseOptions.Move;
            // move the mouse down 100 points
            info.DeltaY = 100;

            InputInjector inputInjector = InputInjector.TryCreate();
            inputInjector.InjectMouseInput(new[] { info });
        }

        private async void TypeText(String input)
        {
            //TypingTarget.Focus(FocusState.Programmatic);
            await Task.Delay(100); //we must yield the UI thread so that focus can be acquired

            InputInjector inputInjector = InputInjector.TryCreate();
            foreach (var letter in input)
            {
                var info = new InjectedInputKeyboardInfo();
                info.VirtualKey = (ushort)((VirtualKey)Enum.Parse(typeof(VirtualKey), letter.ToString(), true));
                inputInjector.InjectKeyboardInput(new[] { info });
                await Task.Delay(100);
            }
        }

        private String GetDate (){

            return DateTime.Now.ToString("M/d/yyyy");

        }
    }
}
