import axios from '../api/axios';
import React, { createContext, useContext, useState, useEffect } from 'react';
import { useUser } from './UserContext';
import Swal from 'sweetalert2';

export const PurchaseContext = createContext(null);

export const usePurchase = () => useContext(PurchaseContext);

export const PurchaseProvider = ({ children }) => {
    const [isActive, setIsActive] = useState(false);
    const { addOrderToUser } = useUser();

    const defaultValue = {
        Sesion: null,
        Pelicula: null,
        Cantidad: 0,
        Precio: 0,
        ButacasSeleccionadas: [],
        PagoId: '',
        UsuarioId: ''
    };

    const [order, setOrder] = useState(defaultValue);
    const [callback, setCallback] = useState(null);

    useEffect(() => {
        if (callback) {
            callback();
            setCallback(null); // Clean up callback after execution
        }
    }, [order, callback]);

    const setScreeningData = (sesion, pelicula, cantidad, precio) => {
        setOrder(prevState => ({ ...prevState, Sesion: sesion, Pelicula: pelicula, Cantidad: cantidad, Precio: precio }));
        setCallback(() => callbackFn);
    };

    const setSeats = (butacasSeleccionadas) => {
        setOrder(prevState => ({ ...prevState, ButacasSeleccionadas: butacasSeleccionadas }));
        setCallback(() => callbackFn);
    };

    const setPaymentId = (pagoId) => {
        setOrder(prevState => ({ ...prevState, PagoId: pagoId }));
        setCallback(() => callbackFn);
    };

    const setUserId = (usuarioId) => {
        setOrder(prevState => ({ ...prevState, UsuarioId: usuarioId }));
    };

    const updateScreening = async (sesionId, butacasSeleccionadas) => {
        try {
            await axios.put(`/sesiones/${sesionId}/updateSeats`, { butacasSeleccionadas });
            Swal.fire({
                icon: 'success',
                title: 'Función actualizada correctamente',
                showConfirmButton: false,
                timer: 1500
            });
        } catch (error) {
            console.error('Error al actualizar la sesión:', error);
            Swal.fire({
                icon: 'error',
                title: 'Error al actualizar la función',
                text: 'Por favor, intenta de nuevo',
            });
        }
    };

    const uploadOrder = async (newOrder) => {
        try {
            const { data } = await axios.post('/ordenes', newOrder);
            setOrder(prevState => ({ ...prevState, OrdenId: data.id }));
            addOrderToUser(data.id);
            Swal.fire({
                icon: 'success',
                title: 'Orden creada exitosamente',
                showConfirmButton: false,
                timer: 1500
            });
        } catch (error) {
            console.error('Error al crear la orden:', error);
            Swal.fire({
                icon: 'error',
                title: 'Error al crear la orden',
                text: 'Por favor, intenta de nuevo',
            });
        }
    };

    const submitOrder = () => {
        const { Pelicula, Sesion, ButacasSeleccionadas, Precio, PagoId, UsuarioId } = order;

        const newOrder = {
            Pelicula,
            Sesion,
            ButacasSeleccionadas,
            Precio,
            PagoId,
            UsuarioId,
            // Agrega más campos si es necesario
        };
        uploadOrder(newOrder);
        updateScreening(Sesion.id, ButacasSeleccionadas);
        setOrder(defaultValue);
    };

    return (
        <PurchaseContext.Provider value={{
            order,
            setScreeningData,
            setSeats,
            setPaymentId,
            setUserId,
            isActive,
            setIsActive,
            submitOrder
        }}>
            {children}
        </PurchaseContext.Provider>
    );
};






// import axios from '../api/axios';
// import React, { useContext, useState, useEffect } from 'react';
// import { useUser } from './UserContext';
// import Swal from 'sweetalert2';
// import PropTypes from 'prop-types';

// const PurchaseContext = React.createContext([]);

// export const usePurchase = () => useContext(PurchaseContext);

// export const PurchaseProvider = ({ children }) => {
//     const [isActive, setIsActive] = useState(false);
//     const { addOrder: addUserOrder } = useUser();

//     const defaultValue = {
//         screening: {},
//         movie: {},
//         cantidad: 0,
//         precio: 0,
//         seatsNumbers: [],
//         paymentId: '',
//         userId: ''
//     };

//     const [order, setOrder] = useState(defaultValue);
//     const [callback, setCallback] = useState(null);

//     useEffect(() => {
//         if (callback) {
//             callback();
//             setCallback(null); // Clean up callback after execution
//         }
//     }, [order, callback]);

//     const setScreeningData = (screening, movie, cantidad, precio, callbackFn) => {
//         setOrder(prevState => ({ ...prevState, screening, movie, cantidad, precio }));
//         setCallback(() => callbackFn);
//     };

//     const setSeats = (seatsNumbers, callbackFn) => {
//         setOrder(prevState => ({ ...prevState, seatsNumbers }));
//         setCallback(() => callbackFn);
//     };

//     const setPaymentId = (paymentId, callbackFn) => {
//         setOrder(prevState => ({ ...prevState, paymentId }));
//         setCallback(() => callbackFn);
//     };

//     const setUserId = (userId) => {
//         setOrder(prevState => ({ ...prevState, userId }));
//     };

//     const setOrderId = (orderId) => {
//         setOrder(prevState => ({ ...prevState, orderId }));
//     };

//     const updateScreening = (screeningId, seatsNumbers) => {
//         axios.put(`/screenings/${screeningId}/updateSeats`, { seatsNumbers })
//             .then(() => {
//                 Swal.fire({
//                     icon: 'success',
//                     title: 'Función actualizada correctamente',
//                     showConfirmButton: false,
//                     timer: 1500
//                 });
//             })
//             .catch(error => {
//                 console.error(error);
//                 Swal.fire({
//                     icon: 'error',
//                     title: 'Error al actualizar la función',
//                     text: 'Por favor, intenta de nuevo',
//                 });
//             });
//     };

//     const uploadOrder = (newOrder) => {
//         axios.post('/orders', newOrder)
//             .then(response => {
//                 const { id } = response.data;
//                 setOrderId(id);
//                 addUserOrder(id);
//                 Swal.fire({
//                     icon: 'success',
//                     title: 'Orden creada exitosamente',
//                     showConfirmButton: false,
//                     timer: 1500
//                 });
//             })
//             .catch(error => {
//                 console.error(error);
//                 Swal.fire({
//                     icon: 'error',
//                     title: 'Error al crear la orden',
//                     text: 'Por favor, intenta de nuevo',
//                 });
//             });
//     };

//     const submitOrder = (currentOrder) => {
//         const { movie, screening, seatsNumbers, precio, paymentId, userId } = currentOrder;
//         const { sala, tipo, lenguaje, horario, id: funcionId } = screening;
//         const { title, id: movieId, poster_path, backdrop_path } = movie;

//         let codigoParaRetirar = `${String(movieId).slice(0, 2)}${funcionId.slice(-2)}${paymentId.slice(-2)}${userId.slice(0, 2)}`.toUpperCase();

//         const orderToSend = {
//             funcion: { sala, tipo, lenguaje, horario, seatsNumbers, funcionId },
//             movie: { title, movieId, poster_path, backdrop_path },
//             precio,
//             paymentId,
//             userId,
//             codigoParaRetirar,
//             fechaDeEmision: new Date()
//         };

//         uploadOrder(orderToSend);
//         updateScreening(funcionId, seatsNumbers);
//         setOrder(defaultValue);
//     };

//     return (
//         <PurchaseContext.Provider value={{ order, setScreeningData, setSeats, setPaymentId, setUserId, isActive, setIsActive, submitOrder }}>
//             {children}
//         </PurchaseContext.Provider>
//     );
// };

// PurchaseProvider.propTypes = {
//     children: PropTypes.node.isRequired
// };
