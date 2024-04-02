import Barcode from 'react-barcode';
import QRCode from 'react-qr-code';

const UserTicket = ({ titulo, imagen, backdrop_path = "../../../public/assets/img/a.webp", idS, lenguaje = "Español", fecha, idB, precio, paymentId, codigoT, ci, fechaDeCompra }) => {


    const formatoTiempo = ( date ) => {
        const hh = ('0' + date.getHours()).slice(-2);
        const mm = ('0' + date.getMinutes()).slice(-2);
        const ss = ('0' + date.getSeconds()).slice(-2);

        return `${hh}:${mm}:${ss}`;
    }

    const fechaDate = fecha.toDate();
    const fechaDeCompraFuncion = fechaDate.toLocaleDateString();
    const horaFuncion = fechaDate.getHours() + ':' + fechaDate.getMinutes();

    const fechaDeCompraPago = fechaDeCompra.toDate().toLocaleDateString();
    const horaPago = formatoTiempo(fechaDeCompra.toDate());

    const backdropPath = `${backdrop_path}`;
    const backgroundStyle = {
        backgroundImage: `url(${backdropPath})`
    }

    return (
        <div className="userTicket">
            <div className="userTicket_left xs:text-md mlg:text-2xl">
                <div className='userTicket_left-background'>
                    <div style={backgroundStyle}></div>
                </div>

                <span className='userTicket_left-order'>{`ORDEN N°: #${ci}`}</span>

                <div className='userTicket_left-items'>
                    <span> {`${idS} (${lenguaje})`} </span>

                    <span>{titulo}</span>

                    <span>{`${fechaDeCompraFuncion} - ${horaFuncion}`}</span>

                    <span>{`Butacas: ${idB.join(', ')}`}</span>

                    <span>{`Total: $${precio}`}</span>
                </div>
            </div>

            <div className="userTicket_right">
                <span className='xs:text-sm uppercase font-semibold underline mb-3 userTicket_right-text'>Código para retirar en el cine</span>
                <div className='userTicket_right-qrCode'>
                    <QRCode
                        size={256}
                        style={{ height: "auto", maxWidth: "100%", width: "100%" }}
                        value={codigoT}
                        viewBox={`0 0 256 256`}
                    />
                </div>
                <span className='font-bowlby text-[#E85D04] mt-2 userTicket_right-text'>{codigoT}</span>
            </div>

            {<div className="userTicket_bottom">
                <div className='userTicket_bottom-text uppercase xs:text-md px-5 flex flex-col'>
                    <span>Código de pago</span>
                    <span>{`(${fechaDeCompraPago} - ${horaPago})`}</span>
                </div>

                <div className='userTicket_bottom-barcode hidden sm:flex'>
                    <Barcode
                        value={paymentId}
                        height={25}
                        width={1}
                        font='Albert Sans'
                        fontSize={15}
                    />
                </div>

                <div className='userTicket_bottom-barcode flex sm:hidden'>
                    <Barcode
                        value={paymentId}
                        height={15}
                        width={.6}
                        font='Albert Sans'
                        fontSize={5}
                    />
                </div>
            </div>}


        </div>
    )
}

export default UserTicket;

// import React from 'react';
// import QRCode from 'react-qr-code';
// import axios from '../../api/axios';
// import Swal from 'sweetalert2';
// import { useUser } from '../context/UserContext';

// const UserTicket = ({ id, movie, funcion, precio, codigoT, fechaDeCompra, onDelete }) => {
//   // Desestructuración de props para fácil acceso
//   const { titulo, posterPath } = movie;
//   const { idS, fecha, idB } = funcion;
//   const { user } = useUser();

//   // Formato de fechaDeCompra y hora
//   const formatDate = (dateString) => {
//     const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
//     return new Date(dateString).toLocaleDateString(undefined, options);
//   };

//   // Función para manejar la eliminación de un ticket
//   const handleDelete = async () => {
//     try {
//       await axios.delete(`/tickets/${id}`, {
//         headers: { Authorization: `Bearer ${user.token}` }
//       });
//       onDelete(id);
//       Swal.fire('Eliminado', 'El ticket ha sido eliminado correctamente.', 'success');
//     } catch (error) {
//       console.error('Error al eliminar el ticket:', error);
//       Swal.fire('Error', 'No se pudo eliminar el ticket.', 'error');
//     }
//   };

//   return (
//     <div className="userTicket">
//       <div className="userTicket-info">
//         <img src={posterPath} alt="Poster" />
//         <div>
//           <h3>{titulo}</h3>
//           <p>idS: {idS}</p>
//           <p>fecha: {formatDate(fecha)}</p>
//           <p>Asientos: {idB.join(', ')}</p>
//           <p>Precio: ${precio}</p>
//           <p>fechaDeCompra de emisión: {formatDate(fechaDeCompra)}</p>
//         </div>
//       </div>
//       <div className="userTicket-qr">
//         <QRCode value={codigoT} />
//       </div>
//       <button onClick={handleDelete}>Eliminar Ticket</button>
//     </div>
//   );
// };

// export default UserTicket;


//................................................................
// import Barcode from 'react-barcode';
// import QRCode from 'react-qr-code';

// const UserTicket = ({ movie, funcion, precio, paymentId, codigoT, id, fechaDeCompra }) => {
//     const { titulo, poster_path, backdrop_path } = movie;
//     const { idS, , lenguaje, fecha, idB } = funcion;

//     const formatoTiempo = ( date ) => {
//         const hh = ('0' + date.getHours()).slice(-2);
//         const mm = ('0' + date.getMinutes()).slice(-2);
//         const ss = ('0' + date.getSeconds()).slice(-2);

//         return `${hh}:${mm}:${ss}`;
//     }

//     const fechaDate = fecha.toDate();
//     const fechaDeCompraFuncion = fechaDate.toLocaleDateString();
//     const horaFuncion = fechaDate.getHours() + ':' + fechaDate.getMinutes();

//     const fechaDeCompraPago = fechaDeCompra.toDate().toLocaleDateString();
//     const horaPago = formatoTiempo(fechaDeCompra.toDate());

//     const backdropPath = `https://image.tmdb.org/t/p/original/${backdrop_path}`;
//     const backgroundStyle = {
//         backgroundImage: `url(${backdropPath})`
//     }

//     return (
//         <div className="userTicket">
//             <div className="userTicket_left xs:text-md mlg:text-2xl">
//                 <div className='userTicket_left-background'>
//                     <div style={backgroundStyle}></div>
//                 </div>

//                 <span className='userTicket_left-order'>{`ORDEN N°: #${id}`}</span>

//                 <div className='userTicket_left-items'>
//                     <span> {`${idS} - ${} (${lenguaje})`} </span>

//                     <span>{titulo}</span>

//                     <span>{`${fechaDeCompraFuncion} - ${horaFuncion}`}</span>

//                     <span>{`Butacas: ${idB.join(', ')}`}</span>

//                     <span>{`Total: $${precio}`}</span>
//                 </div>
//             </div>

//             <div className="userTicket_right">
//                 <span className='xs:text-sm uppercase font-semibold underline mb-3 userTicket_right-text'>Código para retirar en el cine</span>
//                 <div className='userTicket_right-qrCode'>
//                     <QRCode
//                         size={256}
//                         style={{ height: "auto", maxWidth: "100%", width: "100%" }}
//                         value={codigoT}
//                         viewBox={`0 0 256 256`}
//                     />
//                 </div>
//                 <span className='font-bowlby text-[#E85D04] mt-2 userTicket_right-text'>{codigoT}</span>
//                 <button onClick={() => onDelete(id)} className="userTicket_delete-btn">
//                     Eliminar Ticket
//                 </button>
//             </div>

//             {<div className="userTicket_bottom">
//                 <div className='userTicket_bottom-text uppercase xs:text-md px-5 flex flex-col'>
//                     <span>Código de pago</span>
//                     <span>{`(${fechaDeCompraPago} - ${horaPago})`}</span>
//                 </div>

//                 <div className='userTicket_bottom-barcode hidden sm:flex'>
//                     <Barcode
//                         value={paymentId}
//                         height={25}
//                         width={1}
//                         font='Albert Sans'
//                         fontSize={15}
//                     />
//                 </div>

//                 <div className='userTicket_bottom-barcode flex sm:hidden'>
//                     <Barcode
//                         value={paymentId}
//                         height={15}
//                         width={.6}
//                         font='Albert Sans'
//                         fontSize={5}
//                     />
//                 </div>
//             </div>}




//         </div>
//     )
// }

// export default UserTicket;