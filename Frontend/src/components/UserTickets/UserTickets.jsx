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
        axios.delete(`/api/tickets/${ticketId}`)
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



// import { useEffect, useState } from 'react';
// import { scrollTo } from '../Utils/functions';
// import { useUser } from '../../context/UserContext';
// import UserTicket from './UserTicket';
// import Loader from '../Loader/Loader';
// import axios from '../../api/axios';

// const UserTickets = () => {
//     const { user, isLogged } = useUser();
//     const [loading, setLoading] = useState(true);
//     const [userTickets, setUserTickets] = useState([]);

//     // Función para obtener el título e imagen de una película por su ID
//     const getPeliculaById = async (idP) => {
//         try {
//             const { data } = await axios.get(`Pelicula/GetById/${idP}`);
//             return { titulo: data.titulo, imagen: data.imagen };
//         } catch (error) {
//             console.error("Error al obtener detalles de la película:", error);
//             return { titulo: '', imagen: '' }; // Devuelve valores predeterminados en caso de error
//         }
//     };

//     const getUserTickets = async () => {
//         if (!user.id) return;

//         try {
//             const response = await axios.post('Compra/Create', {
//                 ci: user.ci, // Suponiendo que usamos el CI del usuario como identificador
//             });

//             // Obtener tickets y enriquecerlos con los datos de las películas correspondientes
//             const ticketsConDetalles = await Promise.all(response.data.map(async (ticket) => {
//                 const detallesPelicula = await getPeliculaById(ticket.idP);
//                 return { ...ticket, ...detallesPelicula };
//             }));

//             return ticketsConDetalles;
//         } catch (error) {
//             console.error("Error al obtener tickets:", error);
//             throw error;
//         }
//     };

//     useEffect(() => {
//         if (isLogged) {
//             scrollTo('main');
//             setLoading(true);

//             getUserTickets()
//                 .then(tickets => {
//                     setUserTickets(tickets);
//                 })
//                 .catch(err => console.log(err))
//                 .finally(() => setLoading(false));
//         }
//     }, [user, isLogged]);

//     return (
//         <div>
//             {isLogged ? (
//                 <div className='flex flex-col items-center mb-7 pb-10'>
//                     {!loading ? (
//                         <div className='flex flex-col items-center'>
//                             <h1 className="text text-4xl uppercase mb-14 underline font-bowlby">Mis tickets</h1>
//                             <ul className='userTickets'>
//                                 {userTickets.map((ticket, index) => (
//                                     <li key={index}>
//                                         <UserTicket {...ticket} />
//                                     </li>
//                                 ))}
//                             </ul>
//                             {userTickets.length === 0 && <div className='text-center font-albert text-xl'>Aún no ha comprado ninguna entrada</div>}
//                         </div>
//                     ) : (
//                         <Loader />
//                     )}
//                 </div>
//             ) : (
//                 <h1 className='text-center text-2xl'>Debe estar loggeado para poder ver sus tickets</h1>
//             )}
//         </div>
//     );
// };

// export default UserTickets;
