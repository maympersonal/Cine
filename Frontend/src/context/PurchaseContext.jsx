import axios from '../api/axios';
import React, { useContext, useState, useEffect } from 'react';
import { useUser } from './UserContext';
import Swal from 'sweetalert2';
import PropTypes from 'prop-types';

const PurchaseContext = React.createContext([]);

export const usePurchase = () => useContext(PurchaseContext);

export const PurchaseProvider = ({ children }) => {
    const [isActive, setIsActive] = useState(false);
    const { addOrder: addUserOrder } = useUser();

    const defaultValue = {
        screening: {},
        movie: {},
        cantidad: 0,
        precio: 0,
        seatsNumbers: [],
        paymentId: '',
        userId: ''
    };

    const [order, setOrder] = useState(defaultValue);
    const [callback, setCallback] = useState(null);

    useEffect(() => {
        if (callback) {
            callback();
            setCallback(null); // Clean up callback after execution
        }
    }, [order, callback]);

    const setScreeningData = (screening, movie, cantidad, precio, callbackFn) => {
        setOrder(prevState => ({ ...prevState, screening, movie, cantidad, precio }));
        setCallback(() => callbackFn);
    };

    const setSeats = (seatsNumbers, callbackFn) => {
        setOrder(prevState => ({ ...prevState, seatsNumbers }));
        setCallback(() => callbackFn);
    };

    const setPaymentId = (paymentId, callbackFn) => {
        setOrder(prevState => ({ ...prevState, paymentId }));
        setCallback(() => callbackFn);
    };

    const setUserId = (userId) => {
        setOrder(prevState => ({ ...prevState, userId }));
    };

    const setOrderId = (orderId) => {
        setOrder(prevState => ({ ...prevState, orderId }));
    };

    const updateScreening = (screeningId, seatsNumbers) => {
        axios.put(`/screenings/${screeningId}/updateSeats`, { seatsNumbers })
            .then(() => {
                Swal.fire({
                    icon: 'success',
                    title: 'Función actualizada correctamente',
                    showConfirmButton: false,
                    timer: 1500
                });
            })
            .catch(error => {
                console.error(error);
                Swal.fire({
                    icon: 'error',
                    title: 'Error al actualizar la función',
                    text: 'Por favor, intenta de nuevo',
                });
            });
    };

    const uploadOrder = (newOrder) => {
        axios.post('/orders', newOrder)
            .then(response => {
                const { id } = response.data;
                setOrderId(id);
                addUserOrder(id);
                Swal.fire({
                    icon: 'success',
                    title: 'Orden creada exitosamente',
                    showConfirmButton: false,
                    timer: 1500
                });
            })
            .catch(error => {
                console.error(error);
                Swal.fire({
                    icon: 'error',
                    title: 'Error al crear la orden',
                    text: 'Por favor, intenta de nuevo',
                });
            });
    };

    const submitOrder = (currentOrder) => {
        const { movie, screening, seatsNumbers, precio, paymentId, userId } = currentOrder;
        const { sala, tipo, lenguaje, horario, id: funcionId } = screening;
        const { title, id: movieId, poster_path, backdrop_path } = movie;

        let codigoParaRetirar = `${String(movieId).slice(0, 2)}${funcionId.slice(-2)}${paymentId.slice(-2)}${userId.slice(0, 2)}`.toUpperCase();

        const orderToSend = {
            funcion: { sala, tipo, lenguaje, horario, seatsNumbers, funcionId },
            movie: { title, movieId, poster_path, backdrop_path },
            precio,
            paymentId,
            userId,
            codigoParaRetirar,
            fechaDeEmision: new Date()
        };

        uploadOrder(orderToSend);
        updateScreening(funcionId, seatsNumbers);
        setOrder(defaultValue);
    };

    return (
        <PurchaseContext.Provider value={{ order, setScreeningData, setSeats, setPaymentId, setUserId, isActive, setIsActive, submitOrder }}>
            {children}
        </PurchaseContext.Provider>
    );
};

PurchaseProvider.propTypes = {
    children: PropTypes.node.isRequired
};


// import React, { useContext, useState, useEffect } from 'react';
// import { useUser } from './UserContext';
// import Swal from 'sweetalert2';
// import PropTypes from 'prop-types';

// const PurchaseContext = React.createContext([]);

// export const usePurchase = () => useContext(PurchaseContext);

// export const PurchaseProvider = ({ children }) => {
//     const [isActive, setIsActive] = useState(false);
//     const { addOrder: addUserOrder } = useUser();

//     // Datos ficticios para la visualización
//     const mockScreening = {
//         id: 'screening123',
//         sala: 'Sala 3D',
//         tipo: '3D',
//         lenguaje: 'Español',
//         horario: '20:00',
//         asientosDisponibles: 50,
//         asientosOcupados: ['A1', 'A2', 'B1', 'B2']
//     };

//     const mockMovie = {
//         id: 'movie123',
//         title: 'Película Ficticia',
//         poster_path: '/ruta/a/imagen/ficticia.jpg',
//         backdrop_path: '/ruta/a/imagen/fondo/ficticia.jpg',
//     };

//     const defaultValue = {
//         screening: mockScreening,
//         movie: mockMovie,
//         cantidad: 2,
//         precio: 20,
//         seatsNumbers: ['A3', 'A4'],
//         paymentId: 'payment123',
//         userId: 'user123'
//     };

//     const [order, setOrder] = useState(defaultValue);

//     const setScreeningData = (screening, movie, cantidad, precio) => {
//         setOrder(prevState => ({ ...prevState, screening, movie, cantidad, precio }));
//     };

//     const setSeats = (seatsNumbers) => {
//         setOrder(prevState => ({ ...prevState, seatsNumbers }));
//     };

//     const setPaymentId = (paymentId) => {
//         setOrder(prevState => ({ ...prevState, paymentId }));
//     };

//     const setUserId = (userId) => {
//         setOrder(prevState => ({ ...prevState, userId }));
//     };

//     const setOrderId = (orderId) => {
//         setOrder(prevState => ({ ...prevState, orderId }));
//     };

//     const updateScreening = (screeningId, seatsNumbers) => {
//         // Simulación de actualización de función de cine
//         console.log(`Actualizando función: ${screeningId} con asientos: ${seatsNumbers}`);
//         Swal.fire('Función actualizada correctamente', '', 'success');
//     };

//     const uploadOrder = (newOrder) => {
//         // Simulación de subida de orden
//         console.log('Orden creada exitosamente', newOrder);
//         Swal.fire('Orden creada exitosamente', '', 'success');
//         const simulatedResponseId = 'orderId123';
//         setOrderId(simulatedResponseId);
//         addUserOrder(simulatedResponseId);
//     };

//     const submitOrder = () => {
//         // Simulación de envío de orden
//         const orderToSend = { ...order, fechaDeEmision: new Date() };
//         console.log('Enviando orden:', orderToSend);
//         uploadOrder(orderToSend);
//         updateScreening(order.screening.id, order.seatsNumbers);
//         setOrder(defaultValue); // Restablecer al valor por defecto después de enviar
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
