using Wam.Core.Nodes;
using Wam.Core.Plugins;
using Wam.Core.Relations;
using Wam.Core.ValueObjects;

namespace Wam.Plugins.Sample;

/// <summary>
/// Reference plugin demonstrating every extension point: a node type with its own
/// status set, a relation type, and a schema-declared settings page. Copy this repo
/// as the starting point for a new WAM plugin — it depends only on Jaywapp.Wam.Core.
/// </summary>
public sealed class SamplePlugin : IWamPlugin
{
    public const string Id = "wam.plugin.sample";

    public string PluginId => Id;
    public string DisplayName => "Sample Plugin";
    public string Version => "1.0.0";

    public IEnumerable<INodeTypeDefinition> GetNodeTypes() =>
    [
        new NodeTypeDefinition
        {
            NodeTypeId = "sample.ticket",
            DisplayName = "Sample Ticket",
            DefaultColor = "#DB2777",
            Icon = "tag",
            DefaultViewMode = NodeViewMode.Normal,
            AvailableStatuses =
            [
                new StatusDescriptor("Open",     "Open",      "#3B82F6", 0),
                new StatusDescriptor("Triaged",  "Triaged",   "#F59E0B", 1),
                new StatusDescriptor("Resolved", "Resolved",  "#16A34A", 2),
            ],
            DefaultStatus = new StatusDescriptor("Open", "Open", "#3B82F6", 0)
        }
    ];

    public IEnumerable<RelationTypeDefinition> GetRelationTypes() =>
    [
        new RelationTypeDefinition("sample.resolves", "Resolves", RelationDirection.Directed)
    ];

    // Settings are declared as a schema; the host renders the editor and persists
    // values by key. Read them back at runtime with SampleSettings.From(store).
    public IEnumerable<PluginSettingsPage> GetSettingsPages() =>
    [
        new PluginSettingsPage("sample.settings", "Sample Plugin", "Plugins", 100,
        [
            new PluginSettingField("serverUrl", "Server URL", PluginSettingKind.Text,
                DefaultValue: "https://example.com"),
            new PluginSettingField("apiKey", "API Key", PluginSettingKind.Text),
            new PluginSettingField("verbose", "Verbose logging", PluginSettingKind.Boolean,
                DefaultValue: "false"),
        ])
    ];
}

/// <summary>Sample settings POCO, mapped from the values the host persisted.</summary>
public sealed class SampleSettings
{
    public string ServerUrl { get; set; } = "https://example.com";
    public string ApiKey { get; set; } = "";
    public bool Verbose { get; set; }

    public static SampleSettings From(IPluginSettingsStore store) =>
        From(store.Load(SamplePlugin.Id));

    public static SampleSettings From(PluginSettingsValues? v) => new()
    {
        ServerUrl = v?.GetString("serverUrl") ?? "https://example.com",
        ApiKey = v?.GetString("apiKey") ?? "",
        Verbose = v?.GetBool("verbose") ?? false,
    };
}
