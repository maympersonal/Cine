// import React, { useEffect, useState } from 'react';
// import axios from '../../api/axios';
// import { useUser } from '../../context/UserContext';
// import UserTicket from './UserTicket';
// import Loader from '../components/Loader';

// const UserTickets = () => {
//   const { user } = useUser();
//   const [loading, setLoading] = useState(true);
//   const [tickets, setTickets] = useState([]);

//   useEffect(() => {
//     const fetchTickets = async () => {
//       if (!user || !user.id) return;
//       setLoading(true);
//       try {
//         const { data } = await axios.get(`/tickets/user/${user.id}`, {
//           headers: { Authorization: `Bearer ${user.token}` }
//         });
//         setTickets(data);
//       } catch (error) {
//         console.error('Error al obtener tickets:', error);
//       } finally {
//         setLoading(false);
//       }
//     };

//     fetchTickets();
//   }, [user]);

//   const handleDeleteTicket = (deletedTicketId) => {
//     setTickets(tickets.filter(ticket => ticket.id !== deletedTicketId));
//   };

//   return (
//     <div>
//       {loading ? <Loader /> : tickets.map(ticket => (
//         <UserTicket key={ticket.id} {...ticket} onDelete={handleDeleteTicket} />
//       ))}
//     </div>
//   );
// };

// export default UserTickets;




import { useEffect, useState } from 'react';
import { scrollTo } from '../Utils/functions';
import axios from '../../api/axios'; // Asegúrate de que esta ruta sea correcta
import { useUser } from '../../context/UserContext';
import UserTicket from './UserTicket';
import Loader from '../Loader/Loader';

const UserTickets = () => {
    const { user, isLogged } = useUser();
    const [loading, setLoading] = useState(true);
    const [userTickets, setUserTickets] = useState([]);

    // useEffect(() => {
    //     if (user.id) {
    //         scrollTo('main');
    //         setLoading(true);

    //         // Simulamos una llamada a la API con datos falsos
    //         setTimeout(() => {
    //             const mockTickets = [
    //                 {
    //                     id: '1',
    //                     movie: 'Pelicula Falsa 1',
    //                     screening: 'Sala 1 - 20:00',
    //                     date: '2023-01-01',
    //                     quantity: 2,
    //                     price: 20,
    //                 },
    //                 {
    //                     id: '2',
    //                     movie: 'Pelicula Falsa 2',
    //                     screening: 'Sala 2 - 22:00',
    //                     date: '2023-01-02',
    //                     quantity: 3,
    //                     price: 30,
    //                 },
    //                 // Agrega más tickets falsos según sea necesario
    //             ];

    //             setUserTickets(mockTickets);
    //             setLoading(false);
    //         }, 1000); // Retraso simulado
    //     }
    // }, [user]);

    useEffect(() => {
        if (user.id) {
            scrollTo('main');
            setLoading(true);

            axios.get(`/orders/user/${user.id}`)
                .then(response => {
                    setUserTickets(response.data);
                    setLoading(false);
                })
                .catch(error => {
                    console.error('Error al obtener los tickets del usuario:', error);
                    setLoading(false);
                });
        }
    }, [user]);

    // Función para eliminar tickets
    const handleDeleteTicket = (ticketId) => {
        // Aquí llamarías a tu API para eliminar el ticket
        axios.delete(`tickets/${ticketId}`)
            .then(() => {
                // Actualiza el estado para reflejar la eliminación
                const updatedTickets = userTickets.filter(ticket => ticket.id !== ticketId);
                setUserTickets(updatedTickets);
                // Mostrar alguna notificación al usuario
            })
            .catch(error => {
                console.error('Error al eliminar el ticket:', error);
                // Mostrar algún mensaje de error al usuario
            });
    };


    return (
        <div>
            {isLogged ?
                <div className='flex flex-col items-center mb-7 pb-10'>
                    {!loading ?
                        <div className='flex flex-col items-center'>
                            <h1 className="text text-4xl uppercase mb-14 underline font-bowlby">Mis tickets</h1>
                            <ul className='userTickets'>
                                {userTickets.map(ticket => (
                                    <li key={ticket.id}>
                                        <UserTicket {...ticket} onDelete={handleDeleteTicket} />
                                    </li>
                                ))}
                            </ul>

                            {userTickets.length === 0 && <div className='text-center font-albert text-xl'>Aún no ha comprado ninguna entrada.</div>}
                        </div>
                        :
                        <Loader />
                    }
                </div>
                :
                <h1 className='text-center text-2xl'>Debe estar loggeado para poder ver sus tickets.</h1>
            }
        </div>
    );
}

export default UserTickets;
