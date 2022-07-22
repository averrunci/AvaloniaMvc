// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

/// <summary>
/// Provides data for the <see cref="AvaloniaController.UnhandledException"/> event.
/// </summary>
public class UnhandledExceptionEventArgs : EventArgs
{
    /// <summary>
    /// Gets the unhandled exception.
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// Gets or sets the value that indicates whether the exception is handled.
    /// </summary>
    public bool Handled { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnhandledExceptionEventArgs"/> class
    /// with the specified unhandled exception.
    /// </summary>
    /// <param name="exception">The unhandled exception.</param>
    public UnhandledExceptionEventArgs(Exception exception) => Exception = exception;
}