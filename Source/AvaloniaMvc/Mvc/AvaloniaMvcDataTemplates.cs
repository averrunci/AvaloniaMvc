// Copyright (C) 2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

/// <summary>
/// Represents a collection of data templates used by AvaloniaMvc.
/// </summary>
public class AvaloniaMvcDataTemplates : DataTemplateInclude
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AvaloniaMvcDataTemplates"/> class.
    /// </summary>
    public AvaloniaMvcDataTemplates() : base(new Uri("avares://AvaloniaMvc"))
    {
        Source = new Uri("/Resources/Templates.axaml", UriKind.Relative);
    }
}