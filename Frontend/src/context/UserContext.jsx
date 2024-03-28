
import React, { useContext, useState, useRef } from "react";
import axios from '../api/axios';
import Swal from "sweetalert2";
import PropTypes from 'prop-types';

const UserContext = React.createContext([]);

const useUser = () => useContext(UserContext);

const UserProvider = ({ children }) => {
  const defaultValue = {
    ci: '', // Identificador único para Cliente y Usuario
    nombreS: '',
    apellidos: '',
    correo: '',
    contrasena: '',
    puntos: 0,
    codigo: '',
    rol: ''
    // Nota: el campo `confiabilidad` y relaciones como `Compras` y `Tarjeta` no se manejan directamente aquí
  };

  const localStorageUser = localStorage.getItem('activeUser') !== 'undefined' ? localStorage.getItem('activeUser') : null;
  const [user, setUser] = useState(JSON.parse(localStorageUser) || defaultValue);

  const updateLocalStorage = (newState) => {
    localStorage.removeItem('activeUser');
    localStorage.setItem('activeUser', JSON.stringify(newState));
  }

  const isLogged = ((JSON.stringify(user) !== JSON.stringify(defaultValue))) ? true : false;

  const setUserId = (ci) => {
    setUser(prevState => ({ ...prevState, ci }));
  }

  const Toast = Swal.mixin({
    toast: true,
    position: 'bottom-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer)
      toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
  })

  const findUser = (email) => {
    return axios.get(`/usuarios/findUserByEmail`, { params: { correo: email } })
      .then(res => res.data ? res.data : undefined)
      .catch(error => console.log(error));
  }

  const createUser = (newUser, callback) => {
    findUser(newUser.correo)
      .then(res => {
        if (res) {
          Toast.fire({
            icon: 'error',
            title: 'Ese correo ya está registrado.'
          });
        } else {
          axios.post('/usuarios/create', newUser)
            .then(response => {
              const userWithId = { ...newUser, ci: response.data.ci };
              setUser(userWithId);
              updateLocalStorage(userWithId);
              callback();
              Toast.fire({
                icon: 'success',
                title: `Gracias por registrarte ${newUser.nombreS}!`
              });
            })
            .catch(error => console.log(error));
        }
      })
      .catch(err => console.log(err));
  }

  const login = (inUser, callback) => {
    axios.post(`/usuarios/login`, {
      correo: inUser.correo,
      contrasena: inUser.contrasena // Asegúrate de manejar la contraseña de forma segura
    })
    .then(response => {
      const loggedInUser = response.data;
      setUser(loggedInUser);
      updateLocalStorage(loggedInUser);
      callback();
      Toast.fire({
        icon: 'success',
        title: `Bienvenid@ de vuelta ${loggedInUser.nombreS}!`
      });
    })
    .catch(error => {
      console.log(error);
      Toast.fire({
        icon: 'error',
        title: 'Las credenciales son incorrectas o el usuario no existe.'
      });
    });
  };

  const logout = () => {
    Toast.fire({
      icon: 'success',
      title: `Hasta luego ${user.nombreS}!`
    });
    localStorage.removeItem('activeUser');
    setUser(defaultValue);
    window.location.reload();
  };

  const addOrder = (newOrderId) => {
    axios.post(`/usuarios/${user.ci}/addOrder`, { orderId: newOrderId })
    .then(() => {
      Toast.fire({
        icon: 'success',
        title: 'Orden agregada exitosamente.'
      });

    })
    .catch(error => {
      console.log(error);
      Toast.fire({
        icon: 'error',
        title: 'Error al agregar la orden.'
      });
    });
  };


  const modifyUserCart = (newCart) => {
    axios.put(`/usuarios/${user.ci}/modifyCart`, newCart)
    .then(() => {
      Toast.fire({
        icon: 'success',
        title: 'Carrito actualizado correctamente.'
      });
    })
    .catch(error => {
      console.log(error);
      Toast.fire({
        icon: 'error',
        title: 'Error al actualizar el carrito.'
      });
    });
  };

  const userWidgetRef = useRef();

  const context = {
    user,
    isLogged,
    createUser,
    login,
    logout,
    addOrder,
    modifyUserCart,
    userWidgetRef
  };

  return (
    <UserContext.Provider value={context}>
      {children}
    </UserContext.Provider>
  );
}

UserProvider.propTypes = {
  children: PropTypes.node.isRequired,
};

export { useUser, UserProvider };
