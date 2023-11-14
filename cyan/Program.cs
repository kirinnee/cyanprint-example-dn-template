using sulfone_helium;
using sulfone_helium.Domain.Core;
using sulfone_helium.Domain.Core.Questions;

CyanEngine.StartTemplate(args, async (inquirer, determinism) =>
{
    var name = await inquirer.Text("What is your name?");

    var age = await inquirer.Text(new TextQ
    {
        Message = "How old are you?",
        Validate = v =>
        {
            try
            {
                var n = int.Parse(v);
                if (n < 19) return "Age needs to be large than 19";
                if (n > 150) return "Age needs to be less than 150";
                return null;
            }
            catch (FormatException)
            {
                return "Please enter a number";
            }
        },
    });


    return new Cyan
    {
        Processors = new CyanProcessor[]
        {
            new()
            {
                Name = "kirinnee/dotnet-handlebar",
                Files = new CyanGlob[]
                {
                    new()
                    {
                        Exclude = new[] { "scripts", ".github", "cyan.yaml" },
                        Glob = "**/*.*",
                        Type = GlobType.Template,
                    }
                },
                Config = new
                {
                    vars = new
                    {
                        name,
                        age,
                    }
                }
            },
        },
        Plugins = Array.Empty<CyanPlugin>(),
    };
});