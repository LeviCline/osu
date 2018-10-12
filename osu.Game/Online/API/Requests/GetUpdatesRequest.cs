﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using JetBrains.Annotations;
using osu.Framework.IO.Network;
using osu.Game.Online.Chat;

namespace osu.Game.Online.API.Requests
{
    public class GetUpdatesRequest : APIRequest<GetUpdatesResponse>
    {
        private readonly long since;
        private readonly Channel channel;

        public GetUpdatesRequest(long sinceId, [CanBeNull] Channel channel = null)
        {
            this.channel = channel;
            since = sinceId;
        }

        protected override WebRequest CreateWebRequest()
        {
            var req = base.CreateWebRequest();
            if (channel != null) req.AddParameter(@"channel", channel.Id.ToString());
            req.AddParameter(@"since", since.ToString());

            return req;
        }

        protected override string Target => @"chat/updates";
    }
}