using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordJustineBot.Modules
{
    public class Info : ModuleBase<SocketCommandContext>
    {

        [Command("info")]
        [Summary("Getting info for a user")]
        [Alias("Whois")]
        public async Task UserInfo(IGuildUser user)
        {
            var embed = new EmbedBuilder();
            embed.WithThumbnailUrl(user.GetAvatarUrl());
            if (user.Status == UserStatus.Online)
            {
                embed.WithColor(new Color(0, 255, 0));
            }
            if (user.Status == UserStatus.Idle)
            {
                embed.WithColor(new Color(255, 255, 0));
            }
            if (user.Status == UserStatus.DoNotDisturb)
            {
                embed.WithColor(new Color(255, 0, 0));
            }
            if (user.Status == UserStatus.AFK)
            {
                embed.WithColor(new Color(220, 20, 60));
            }
            if (user.Status == UserStatus.Invisible)
            {
                embed.WithColor(new Color(128, 128, 128));
            }
            if (user.Status == UserStatus.Offline)
            {
                embed.WithColor(new Color(192, 192, 192));
            }

            string nickname = user.Nickname;
            if (string.IsNullOrEmpty(nickname))
            {
                nickname = user.Username;
            }

            string game = user.Game.ToString();
            if (string.IsNullOrEmpty(game))
            {
                game = "Currently not playing";
            }
            embed.AddField("Name", $"**{user}**", true);
            embed.AddField("ID", $"**{user.Id}**", true);
            embed.AddField("Joind Discord at", $"**{user.CreatedAt}**", true);
            embed.AddField($"Joind {Context.Guild}", $"**{user.JoinedAt}**", true);
            embed.AddField("Nickname", $"**{nickname}**", true);
            embed.AddField("Playing", $"**{game}**", true);
            embed.AddField("Status", $"**{user.Status}**", true);
            await ReplyAsync("", false, embed);
        }


    }
}