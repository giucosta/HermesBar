﻿using DAO.Usuario;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Usuario
{
    public class UsuarioBLL
    {
        private UsuarioDAO _usuarioDao;
        public UsuarioDAO UsuarioDao
        {
            get
            {
                if(_usuarioDao == null)
                    _usuarioDao = new UsuarioDAO();
                return _usuarioDao;
            }
        }
        public List<UsuarioModel> PesquisaUsuario(UsuarioModel usuario)
        {
            return UsuarioDao.PesquisaUsuario(usuario);
        }
    }
}
