﻿using System.Text;

namespace Ahu.Business.Helper;

public class TokenEncoderDecoder
{
    public string EncodeToken(string token)
    {
        byte[] tokenBytes = Encoding.UTF8.GetBytes(token);

        string encodedToken = Convert.ToBase64String(tokenBytes);

        encodedToken = encodedToken.Replace('+', '-').Replace('/', '_');

        return encodedToken;
    }

    public string DecodeToken(string encodedToken)
    {
        encodedToken = encodedToken.Replace('-', '+').Replace('_', '/');

        while (encodedToken.Length % 4 != 0)
            encodedToken += '=';

        byte[] tokenBytes = Convert.FromBase64String(encodedToken);

        string token = Encoding.UTF8.GetString(tokenBytes);

        return token;
    }
}