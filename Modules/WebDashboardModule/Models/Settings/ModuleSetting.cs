namespace BonusBot.WebDashboardModule.Models.Settings
{
    public record ModuleSetting
    {
        public string Name { get; init; }
        public string Value { get; set; } = string.Empty;

        public ModuleSetting(string name) 
            => Name = name;
    }
}
