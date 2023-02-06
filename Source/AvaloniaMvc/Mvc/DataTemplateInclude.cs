// Copyright (C) 2020-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;

namespace Charites.Windows.Mvc;

/// <summary>
/// Includes a data template from a URL.
/// </summary>
public class DataTemplateInclude : IDataTemplate
{
    /// <summary>
    /// Gets or Sets the source URL.
    /// </summary>
    public Uri? Source { get; set; }

    /// <summary>
    /// Gets the loaded data templates.
    /// </summary>
    protected AvaloniaList<IDataTemplate> LoadedDataTemplates
    {
        get
        {
            if (loadedDataTemplates is not null) return loadedDataTemplates;

            loadedDataTemplates = LoadDataTemplates();
            return loadedDataTemplates;
        }
    }
    private AvaloniaList<IDataTemplate>? loadedDataTemplates;

    private readonly Uri? baseUri;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTemplateInclude"/> class
    /// with the specified base URL of a data template.
    /// </summary>
    /// <param name="baseUri">The base url of a data template</param>
    public DataTemplateInclude(Uri? baseUri) => this.baseUri = baseUri;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataTemplateInclude"/> class
    /// with the specified service provider.
    /// </summary>
    /// <param name="serviceProvider">The provider that provides the <see cref="IUriContext"/>.</param>
    public DataTemplateInclude(IServiceProvider serviceProvider) : this((serviceProvider.GetService(typeof(IUriContext)) as IUriContext)?.BaseUri)
    {
    }

    /// <summary>
    /// Loads data templates from the source URL.
    /// </summary>
    /// <returns>The data templates loaded from the source URL.</returns>
    protected virtual AvaloniaList<IDataTemplate> LoadDataTemplates()
    {
        if (Source is null) throw new InvalidOperationException("Source must be specified.");
        
        var loadedResource = AvaloniaXamlLoader.Load(Source, baseUri);

        var dataTemplates = new AvaloniaList<IDataTemplate>();
        switch (loadedResource)
        {
            case AvaloniaList<IDataTemplate> list: dataTemplates.AddRange(list);
                break;
            case IDataTemplate dataTemplate: dataTemplates.Add(dataTemplate);
                break;
        }

        return dataTemplates;
    }

    /// <summary>
    /// Creates the control.
    /// </summary>
    /// <param name="param">The parameter.</param>
    /// <returns>The created control.</returns>
    protected virtual Control? Build(object? param)
        => LoadedDataTemplates.FirstOrDefault(dataTemplate => dataTemplate.Match(param))?.Build(param);

    /// <summary>
    /// Checks to see if this data template matches the specified data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns><c>true</c> if the data template can build a control for the data; otherwise <c>false</c>.</returns>
    protected virtual bool Match(object? data) => LoadedDataTemplates.Any(dataTemplate => dataTemplate.Match(data));

    Control? ITemplate<object?, Control?>.Build(object? param) => Build(param);
    bool IDataTemplate.Match(object? data) => Match(data);
}