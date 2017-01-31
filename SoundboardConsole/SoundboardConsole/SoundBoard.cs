using System.Media;
using System.Threading;
using NAudio.Midi;

namespace SoundboardConsole {
    class SounBoard{
        static SoundPlayer player;
        static void Main(){
            MidiIn midiIN = new MidiIn(0);
            midiIN.Start();
            midiIN.MessageReceived += midIn_MessageReceived;
            new ManualResetEvent(false).WaitOne();
        }

        private static void midIn_MessageReceived(object sender, MidiInMessageEventArgs e){
            if(e.MidiEvent != null && e.MidiEvent.CommandCode == MidiCommandCode.AutoSensing)
                return;
            if(e.MidiEvent != null && e.MidiEvent.CommandCode == MidiCommandCode.NoteOn){
                var ne = (NoteOnEvent)e.MidiEvent;
                try{
                    string path = $@"D:\Ben\Soundboard\sound{ne.NoteNumber}.wav";
                    player = new SoundPlayer(path);
                    player.Play();
                    player.Dispose();
                }
                catch{/*Ignored*/}
            }
        }
    }
}