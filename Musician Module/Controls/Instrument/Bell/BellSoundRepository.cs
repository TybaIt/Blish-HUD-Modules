using System;
using System.Collections.Generic;
using Blish_HUD.Controls.Intern;
using CSCore.Codecs.OGG;
namespace Nekres.Musician_Module.Controls.Instrument
{
    public class BellSoundRepository : IDisposable
    {
        private readonly Dictionary<string, string> Map = new Dictionary<string, string>
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{BellNote.Octaves.Low}", "D4"},
            {$"{GuildWarsControls.WeaponSkill2}{BellNote.Octaves.Low}", "E4"},
            {$"{GuildWarsControls.WeaponSkill3}{BellNote.Octaves.Low}", "F4"},
            {$"{GuildWarsControls.WeaponSkill4}{BellNote.Octaves.Low}", "G4"},
            {$"{GuildWarsControls.WeaponSkill5}{BellNote.Octaves.Low}", "A4"},
            {$"{GuildWarsControls.HealingSkill}{BellNote.Octaves.Low}", "B4"},
            {$"{GuildWarsControls.UtilitySkill1}{BellNote.Octaves.Low}", "C5"},
            {$"{GuildWarsControls.UtilitySkill2}{BellNote.Octaves.Low}", "D5"},
            // Middle Octave
            {$"{GuildWarsControls.WeaponSkill1}{BellNote.Octaves.Middle}", "D5"},
            {$"{GuildWarsControls.WeaponSkill2}{BellNote.Octaves.Middle}", "E5"},
            {$"{GuildWarsControls.WeaponSkill3}{BellNote.Octaves.Middle}", "F5"},
            {$"{GuildWarsControls.WeaponSkill4}{BellNote.Octaves.Middle}", "G5"},
            {$"{GuildWarsControls.WeaponSkill5}{BellNote.Octaves.Middle}", "A5"},
            {$"{GuildWarsControls.HealingSkill}{BellNote.Octaves.Middle}", "B5"},
            {$"{GuildWarsControls.UtilitySkill1}{BellNote.Octaves.Middle}", "C6"},
            {$"{GuildWarsControls.UtilitySkill2}{BellNote.Octaves.Middle}", "D6"},
            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{BellNote.Octaves.High}", "D6"},
            {$"{GuildWarsControls.WeaponSkill2}{BellNote.Octaves.High}", "E6"},
            {$"{GuildWarsControls.WeaponSkill3}{BellNote.Octaves.High}", "F6"},
            {$"{GuildWarsControls.WeaponSkill4}{BellNote.Octaves.High}", "G6"},
            {$"{GuildWarsControls.WeaponSkill5}{BellNote.Octaves.High}", "A6"},
            {$"{GuildWarsControls.HealingSkill}{BellNote.Octaves.High}", "B6"},
            {$"{GuildWarsControls.UtilitySkill1}{BellNote.Octaves.High}", "C7"},
            {$"{GuildWarsControls.UtilitySkill2}{BellNote.Octaves.High}", "D7"}
        };


        private readonly Dictionary<string, OggSource> Sound = new Dictionary<string, OggSource>
        {
            {"D4", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\D4.ogg"))},
            {"E4", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\E4.ogg"))},
            {"F4", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\F4.ogg"))},
            {"G4", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\G4.ogg"))},
            {"A4", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\A4.ogg"))},
            {"B4", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\B4.ogg"))},
            {"C5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\C5.ogg"))},
            {"D5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\D5.ogg"))},
            {"E5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\E5.ogg"))},
            {"F5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\F5.ogg"))},
            {"G5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\G5.ogg"))},
            {"A5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\A5.ogg"))},
            {"B5", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\B5.ogg"))},
            {"C6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\C6.ogg"))},
            {"D6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\D6.ogg"))},
            {"E6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\E6.ogg"))},
            {"F6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\F6.ogg"))},
            {"G6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\G6.ogg"))},
            {"A6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\A6.ogg"))},
            {"B6", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\B6.ogg"))},
            {"C7", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\C7.ogg"))},
            {"D7", new OggSource(MusicianModule.ModuleInstance.ContentsManager.GetFileStream(@"instruments\Bell\D7.ogg"))}

        };


        public OggSource Get(string id)
        {
            return Sound[id];
        }


        public OggSource Get(GuildWarsControls key, BellNote.Octaves octave)
        {
            return Sound[Map[$"{key}{octave}"]];
        }


        public void Dispose() {
            Map?.Clear();
            foreach (var snd in Sound)
                snd.Value?.Dispose();
        }
    }
}