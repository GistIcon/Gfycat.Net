﻿namespace Gfycat
{
    public enum UploadTask
    {
        Invalid, // if for some reason json.net just can't figure it out or they didn't give us anything 
        NotFoundo, // I'm so serious, that's how it's spelled...
        Encoding,
        Error,
        Complete
    }
}