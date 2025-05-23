﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CopyPaste
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            RegisterServices(builder.Services);

            return builder.Build();
        }

        private static void RegisterServices(IServiceCollection container)
        {
            new Services.Core.DependencyRegistrant().Register(container);
            new Services.DependencyRegistrant().Register(container);
        }
    }
}
