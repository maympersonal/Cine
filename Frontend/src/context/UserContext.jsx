import React, { useContext, useState, useRef } from "react";
import axios from '../api/axios';
import Swal from "sweetalert2";
import PropTypes from 'prop-types';

const UserContext = React.createContext([]);

const useUser = () => {
    return useContext(UserContext);
}

const UserProvider = ({ children }) => {
    const defaultValue = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        rol:''
        
    };

    const localStorageUser = localStorage.getItem('activeUser') !== 'undefined' ? localStorage.getItem('activeUser') : null;
    const [user, setUser] = useState(JSON.parse(localStorageUser) || defaultValue);

    const updateLocalStorage = (newState) => {
        localStorage.removeItem('activeUser');
        localStorage.setItem('activeUser', JSON.stringify(newState));
    }

    const isLogged = ((JSON.stringify(user) !== JSON.stringify(defaultValue))) ? true : false;

    const setUserId = (id) => {
        setUser(prevState => ({ ...prevState, id }));
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
        return axios.get(`/users/findUserByEmail`, { params: { email } })
            .then(res => res.data ? res.data : undefined)
            .catch(error => console.log(error));
    }

    const createUser = (newUser, callback) => {
        axios.post('/api/usuario/create', newUser)
            .then(response => {
                const userWithRol = { ...newUser, rol: response.data.rol };
                setUser(userWithRol);
                updateLocalStorage(userWithRol);
                callback();
                Toast.fire({
                    icon: 'success',
                    title: `Gracias por registrarte ${newUser.firstName}!`
                });
            })
            .catch(error => console.log(error));
    }

    const login = (inUser, callback) => {
        findUser(inUser.email)
            .then(res => {
                if (!res) {
                    Toast.fire({
                        icon: 'error',
                        title: 'No hay ningún usuario registrado con ese mail.'
                    });
                } else if (res.password !== inUser.password) {
                    Toast.fire({
                        icon: 'error',
                        title: 'La contraseña es incorrecta.'
                    });
                } else {
                    setUser(res);
                    updateLocalStorage(res);
                    callback();
                    Toast.fire({
                        icon: 'success',
                        title: `Bienvenid@ de vuelta ${res.firstName}!`
                    });
                }
            })
            .catch(err => console.log(err));
    }

    const logout = () => {
        Toast.fire({
            icon: 'success',
            title: `Hasta luego ${user.firstName}!`
        });
        localStorage.removeItem('cart');
        setUser(defaultValue);
        updateLocalStorage(defaultValue);
        window.location.reload();
    }

    const addOrder = (newOrderId) => {
        axios.post(`/users/${user.id}/addOrder`, { orderId: newOrderId })
            .then(response => {
                const updatedOrders = [...user.orders, newOrderId];
                const updatedUser = { ...user, orders: updatedOrders };
                setUser(updatedUser);
                updateLocalStorage(updatedUser);
                Toast.fire({
                    icon: 'success',
                    title: 'Pedido agregado correctamente.'
                });
            })
            .catch(error => {
                console.log(error);
                Toast.fire({
                    icon: 'error',
                    title: 'Ocurrió un error al agregar el pedido.'
                });
            });
    }

    const modifyUserCart = (newCart) => {
        axios.put(`/users/${user.id}/modifyCart`, newCart)
            .then(response => {
                const updatedUser = { ...user, cart: newCart };
                setUser(updatedUser);
                updateLocalStorage(updatedUser);
                Toast.fire({
                    icon: 'success',
                    title: 'Carrito actualizado correctamente.'
                });
            })
            .catch(error => {
                console.log(error);
                Toast.fire({
                    icon: 'error',
                    title: 'Ocurrió un error al actualizar el carrito.'
                });
            });
    }

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
