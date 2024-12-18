using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class TokenLog
    {
        TokenDat tokenDat = new TokenDat();
        public bool saveToken(string hash, string correo, DateTime _fechaInicio, DateTime _fechaFin)
        {
            return tokenDat.saveToken(hash, correo, _fechaInicio, _fechaFin);   
        }

        public DataSet showTokenByHash(string hash)
        {
            return tokenDat.showTokenByHash(hash);
        }

        public int validateTokenExpiration(string correo, DateTime fechaEntrada)
        {
            return tokenDat.validateTokenExpiration(correo, fechaEntrada);
        }

        public int validateEmail(string correo)
        {
            return tokenDat.validateEmail(correo);
        }

        public bool updatePassword(string correo, string salt, string password)
        {
            return tokenDat.updatePassword(correo, salt, password);
        }
    }
}