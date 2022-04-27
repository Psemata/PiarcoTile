﻿using Android.Content.Res;
using PiarcoTile.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PiarcoTile.Models
{
    public class Song
    {
        public string Name { get; set; }
        string Artist { get; set; }
        int ID { get; set; }
        string Music { get; set; }
        public List<Map> Maps { get; set; }

        public Song(int id, string name, string artist, string music, string path, IAssetService assets)
        {
            this.ID = id;
            this.Name = name;
            this.Artist = artist;
            this.Music = music;
            this.Maps = new List<Map>();
            GenerateMaps(path, assets);
        }

        private void GenerateMaps(string path, IAssetService assets)
        {
            string[] c = assets.GetAssetList(path);
            Regex rx = new Regex(@"\[(.+)\]");
            foreach (string s in c)
            {
                if (s.Contains(".osu"))
                {
                    MatchCollection matches = rx.Matches(s);
                    GroupCollection groups = matches[0].Groups;
                    Map m = new Map(groups[1].Value,assets.Open(path+"/"+s));
                    Maps.Add(m);
                }
            }
        }
    }
}
