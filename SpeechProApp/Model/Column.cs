using System;
using System.Data;

namespace SpeechProApp.Model
{
    public class Column
    {
        public string Name { get; set; }
        public Type DotnetType { get; set; }
        public SqlDbType SqlType { get; set; }
    }
}