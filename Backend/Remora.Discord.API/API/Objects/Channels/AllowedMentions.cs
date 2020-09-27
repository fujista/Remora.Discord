//
//  AllowedMentions.cs
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

using System.Collections.Generic;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.Core;

namespace Remora.Discord.API.Objects
{
    /// <inheritdoc />
    public class AllowedMentions : IAllowedMentions
    {
        /// <inheritdoc />
        public Optional<IReadOnlyList<MentionType>> Parse { get; }

        /// <inheritdoc />
        public Optional<IReadOnlyList<Snowflake>> Roles { get; }

        /// <inheritdoc />
        public Optional<IReadOnlyList<Snowflake>> Users { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowedMentions"/> class.
        /// </summary>
        /// <param name="parse">The mention types to parse.</param>
        /// <param name="roles">The roles that may be mentioned.</param>
        /// <param name="users">The users that may be mentioned.</param>
        public AllowedMentions
        (
            Optional<IReadOnlyList<MentionType>> parse,
            Optional<IReadOnlyList<Snowflake>> roles,
            Optional<IReadOnlyList<Snowflake>> users
        )
        {
            this.Parse = parse;
            this.Roles = roles;
            this.Users = users;
        }
    }
}