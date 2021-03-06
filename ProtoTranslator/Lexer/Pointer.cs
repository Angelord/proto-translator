﻿using System;
using System.IO;
using System.Text;

namespace ProtoTranslator.Lexer {
    public class Pointer {

        private readonly StreamReader stream;
        private readonly StringBuilder scanned;
        private int lines = 1;

        public int Lines => lines;

        public char Current => scanned[0];

        public char Next => scanned.Length > 1 ? scanned[1] : (char)stream.Peek();

        public bool IsAtWhitespace => Current == '\n' || Current == ' ' || Current == '\t' || Current == '\r';

        public bool EndOfStream => StreamIsEmpty && scanned.Length == 0;
        
        private bool StreamIsEmpty => stream.Peek() < 0;

        public Pointer(StreamReader stream) {
            this.stream = stream;
            scanned = new StringBuilder();
            scanned.Append(' ');
        }

        /// <summary>
        /// Returns a range of characters as a string, starting at the current one.
        /// If there are not enough characters before the end of the file it will return all remaining ones.
        /// </summary>
        /// <param name="count">The number of characters to return</param>
        public string Select(int count) {

            while (scanned.Length < count && !StreamIsEmpty) {
                scanned.Append((char)stream.Read());
            }

            int countToReturn = Math.Min(scanned.Length, count);
            
            return scanned.ToString(0, countToReturn);
        }

        public bool Move() {
            
            scanned.Remove(0, 1);

            if (!StreamIsEmpty) {
                scanned.Append((char)stream.Read());
            }
            else if(scanned.Length == 0) {    
                return false; // No more scanned symbols remaining
            }
            
            if (Current == '\n') { lines++; }

            return true;
        }
    }
}