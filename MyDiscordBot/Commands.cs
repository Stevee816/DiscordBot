using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using MyDiscordBot;
//using Nozomi.Core.Users;

namespace Nozomi.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("developer")]
        public async Task developer()
        {
            await Context.Channel.SendMessageAsync(Utilities.GetAlert("developer"));
        }


        [Command("dm")]
        public async Task sendmsgtoowner([Remainder] string text)
        {
            if (text.Length >= 400)
            {
                await Context.Channel.SendMessageAsync("Pardon, but your vote must not be more than 400 characters");
                return;
            }

            var embed = new EmbedBuilder()
            {
                Color = new Color(0, 255, 0)
            };
            var application = await Context.Client.GetApplicationInfoAsync();
            var user = application.Owner.GetOrCreateDMChannelAsync();
            var z = await application.Owner.GetOrCreateDMChannelAsync();
            embed.Title = $"Incoming Message..."; ///title///
            embed.Description = $"Username:\n\n {Context.User.Username}\n\nDiscord Name:\n {Context.Guild.Name}\n\nMessage:\n{text}";
            embed.WithThumbnailUrl("http://cdn.bigfm.de/sites/default/files/styles/1240w/public/scald/image/Screen_Shot_2017-08-13_at_11.22.19.png?itok=gYmHY0yx%22");
            await z.SendMessageAsync("", false, embed);

            //Delete the command message from the user
            await Context.Message.DeleteAsync();
        }


        [Command("hello")]
        public async Task hello([Remainder] string message)
        {
            await Context.Channel.SendMessageAsync(message + Context.User.Username);
        }


        [Command("say")]
        public async Task say([Remainder] string message)
        {
            await Context.Channel.SendMessageAsync(message);
        }


        [Command("pick")]
        public async Task pick([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Random r = new Random();
            string seletion = options[r.Next(0, options.Length)];

            var embed = new EmbedBuilder();
            embed.WithAuthor(" :thinking: I choose ");
            embed.WithDescription(seletion);
            embed.WithColor(new Color(255, 255, 0));
            embed.WithThumbnailUrl("https://vignette.wikia.nocookie.net/creepypasta/images/e/e2/Anime-Girl-With-Silver-Hair-And-Purple-Eyes-HD-Wallpaper.jpg/revision/latest?cb=20140120061808");
            await Context.Channel.SendMessageAsync("", false, embed);
        }


        [Command("Avatar")]
        [Summary("Get the user avatar mentioned")]
        [RequireBotPermission(ChannelPermission.SendMessages)]
        public async Task Avatar(IGuildUser user)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle($"Here is the avatar for: {user}");
            embed.WithImageUrl(user.GetAvatarUrl());
            embed.WithColor(new Color(74, 144, 226));
            await Context.Channel.SendMessageAsync("", false, embed);
        }


        [Command("secret")]
        public async Task RevealSecret([Remainder]string arg = "")
        {
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(Utilities.GetAlert("developer"));
        }


        [Command("kick")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser user, string reason)
        {
            if (user == null) throw new ArgumentException("You must mention a user");
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("You must provide a reason");
            var kick = Context.Guild as SocketGuild;
            var embed = new EmbedBuilder();
            embed.Title = $" {user.Username} has been kicked from {user.Guild.Name}";
            embed.WithColor(new Color(56, 144, 226));
            embed.WithImageUrl("http://img1.ak.crunchyroll.com/i/spire3/04152008/b/a/7/2/ba724bbe815c20_full.jpg");
            embed.Title = $"**{user.Username}** was banned";
            embed.Description = $"**Username: **{user.Username}\n**Server Name: **{user.Guild.Name}\n**Banned by: **{Context.User.Mention}!\n**Reason: **{reason}";
            await user.KickAsync();
            await Context.Channel.SendMessageAsync("", false, embed);
        }


        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanAsync(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            if (user == null) throw new ArgumentException("You must mention a user");
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("You must provide a reason");
            var ban = Context.Guild as SocketGuild;
            var embed = new EmbedBuilder();
            embed.WithImageUrl("https://i.pinimg.com/474x/ce/f7/31/cef731fd7402a75e8977317c9b3491e9.jpg");
            embed.WithColor(new Color(56, 144, 226));
            embed.Title = $"**{user.Username}** was banned";
            embed.Description = $"**Username: **{user.Username}\n**Server Name: **{user.Guild.Name}\n**Banned by: **{Context.User.Mention}!\n**Reason: **{reason}";
            await user.Guild.AddBanAsync(user, 5, reason);
            await Context.Channel.SendMessageAsync("", false, embed);
        }


        //[Command("Warn")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //[RequireBotPermission(GuildPermission.BanMembers)]
        //public async Task WarnUser(IGuildUser user)
        //{
        //    var userAccount = UserAccounts.GetAccount((SocketUser)user);
        //    userAccount.NumberOfWarnings++;
        //    UserAccounts.SaveAccounts();

        //    if (userAccount.NumberOfWarnings >= 3)
        //    {
        //        var embed = new EmbedBuilder();
        //        embed.Description = $"{user.Username} has been Bannd";
        //        await Context.Channel.SendMessageAsync("", false, embed);
        //        await user.Guild.AddBanAsync(user, 5);
        //    }
        //    else if (userAccount.NumberOfWarnings == 2)
        //    {
        //        var embed = new EmbedBuilder();
        //        embed.Description = $"{user.Username} has been Warn twice";
        //        await Context.Channel.SendMessageAsync("", false, embed);
        //    }
        //    else if (userAccount.NumberOfWarnings == 1)
        //    {
        //        var embed = new EmbedBuilder();
        //        embed.Description = $"{user.Username} has been Warn once";
        //        await Context.Channel.SendMessageAsync("", false, embed);
        //    }
        //}


        [Command("mute")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        [RequireBotPermission(GuildPermission.MuteMembers)]
        public void Mut(SocketGuildUser user)
        {
            var mute = Context.Guild as SocketGuild;
            var embed = new EmbedBuilder();
            embed.Title = $"**{user.Username}** was muted";
            embed.WithColor(new Color(56, 144, 226));


        }


        [Command("Ping")]
        [Alias("ping", "pong")]
        [Summary("Returns a pong")]
        public async Task Say()
        {
            await ReplyAsync("Pong!");
        }


        [Command("clear", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task clearmsgs(uint amount)
        {
            if (amount > 0)
            {
                var messages = await Context.Channel.GetMessagesAsync((int)amount + 1).Flatten();

                await Context.Channel.DeleteMessagesAsync(messages);
                const int delay = 5000;
                var m = await ReplyAsync(
                    $"Cleaning Complete. _This message will be deleted in {delay / 1000} seconds._");
                await Task.Delay(delay);
                await m.DeleteAsync();
            }
            else
            {
                await Context.Channel.SendMessageAsync(
                    "You need to measure a number greater than _0_, to be deleted");
            }
        }


        //[Command("XP")]
        //public async Task XP()
        //{
        //    var account = UserAccounts.GetAccount(Context.User);
        //    var embed = new EmbedBuilder();
        //    embed.WithColor(new Color(56, 144, 226));
        //    embed.Title = $"**XP**";
        //    embed.Description = $"**Username: {Context.User.Username} ** \n**XP**{account.XP} \n**XP Points **{account.Points} \n**Lavle **{account.LevelNumber}";
        //    await Context.Channel.SendMessageAsync("", false, embed);
        //}

        //[Command("addXP")]
        //[RequireUserPermission(GuildPermission.Administrator)]
        //public async Task AddXP(uint xp)
        //{
        //    var account = UserAccounts.GetAccount(Context.User);
        //    account.XP += xp;
        //    UserAccounts.SaveAccounts();
        //    await Context.Channel.SendMessageAsync($"You gained {xp} XP.");
        //}
    }
}
