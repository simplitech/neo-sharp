﻿using NeoSharp.Application.Attributes;
using System;

namespace NeoSharp.Application.Client
{
    public enum ConsoleOutputStyle : byte
    {
        /// <summary>
        /// Regular output
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.White)]
        Output,
        /// <summary>
        /// Prompt
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.DarkGreen)]
        Prompt,
        /// <summary>
        /// Input
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.Green)]
        Input,
        /// <summary>
        /// Information
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.Yellow)]
        Information,
        /// <summary>
        /// Autocomplete
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.DarkYellow)]
        Autocomplete,
        /// <summary>
        /// Autocomplete match
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.Yellow)]
        AutocompleteMatch,
        /// <summary>
        /// Log
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.Blue)]
        Log,
        /// <summary>
        /// Error
        /// </summary>
        [ConsoleOutputStyle(Foreground = ConsoleColor.Red)]
        Error
    }
}