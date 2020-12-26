//
//  RequireContextCondition.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Threading;
using System.Threading.Tasks;
using Remora.Commands.Conditions;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Commands.Contexts;
using Remora.Results;
using static Remora.Discord.API.Abstractions.Objects.ChannelType;

namespace Remora.Discord.Commands.Conditions
{
    /// <summary>
    /// Checks required contexts before allowing execution.
    /// </summary>
    public class RequireContextCondition : ICondition<RequireContextAttribute>
    {
        private readonly ICommandContext _context;
        private readonly IDiscordRestChannelAPI _channelAPI;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequireContextCondition"/> class.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="channelAPI">The channel API.</param>
        public RequireContextCondition
        (
            ICommandContext context,
            IDiscordRestChannelAPI channelAPI
        )
        {
            _context = context;
            _channelAPI = channelAPI;
        }

        /// <inheritdoc />
        public async ValueTask<DetermineConditionResult> CheckAsync(RequireContextAttribute attribute, CancellationToken ct)
        {
            var getChannel = await _channelAPI.GetChannelAsync(_context.ChannelID, ct);
            if (!getChannel.IsSuccess)
            {
                return DetermineConditionResult.FromError(getChannel);
            }

            var channel = getChannel.Entity;

            switch (attribute.Context)
            {
                case ChannelContext.DM:
                {
                    return channel.Type is DM
                        ? DetermineConditionResult.FromSuccess()
                        : DetermineConditionResult.FromError("This command can only be used in a DM.");
                }
                case ChannelContext.GroupDM:
                {
                    return channel.Type is GroupDM
                        ? DetermineConditionResult.FromSuccess()
                        : DetermineConditionResult.FromError("This command can only be used in a group DM.");
                }
                case ChannelContext.Guild:
                {
                    return channel.Type is GuildText or GuildVoice or GuildCategory or GuildNews or GuildStore
                        ? DetermineConditionResult.FromSuccess()
                        : DetermineConditionResult.FromError("This command can only be used in a guild.");
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
