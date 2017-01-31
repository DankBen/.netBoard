using System.Media;
using System.Threading;
using NAudio.Midi;

namespace SoundboardConsole {
    class SoundBoard {
        static SoundPlayer player;
        static void Main() {
            MidiIn midiIN = new MidiIn(0);
            midiIN.Start();
            midiIN.MessageReceived += midIn_MessageReceived;    //Start midi and add listener delegate
            new ManualResetEvent(false).WaitOne();              //We make sure the application does not terminate
        }

        private static void midIn_MessageReceived(object sender, MidiInMessageEventArgs e) {
            if (e.MidiEvent != null && e.MidiEvent.CommandCode == MidiCommandCode.AutoSensing)
                return;                                         //We don't want to deal with midi events not carrying notes
            if (e.MidiEvent != null && e.MidiEvent.CommandCode == MidiCommandCode.NoteOn) {
                var ne = (NoteOnEvent)e.MidiEvent;
                try {
                    string path = $@"D:\Ben\Soundboard\sound{ne.NoteNumber}.wav";
                    player = new SoundPlayer(path);
                    player.Play();
                    player.Dispose();                           //Initiate player, fire and forget
                }
                catch {/*Ignored*/}
            }
        }
    }
}