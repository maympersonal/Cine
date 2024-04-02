import { useEffect, useState } from "react";
import axios from '../../api/axios';

const MovieScreeningSelect = ({ movieId, setScreeningId, setAvailableSeats }) => {
    const [screenings, setScreenings] = useState([]);
    const [salas, setSalas] = useState([]);
    const [selectedSala, setSelectedSala] = useState('');

    useEffect(() => {
        const fetchScreenings = async () => {
            try {
                const { data } = await axios.get(`Sesion/GetById/${movieId}`);
                const validScreenings = data.filter(sesion => new Date(sesion.fecha) >= new Date());
                setScreenings(validScreenings);

                // Obtener la información de las salas
                const salaIds = [...new Set(validScreenings.map(sesion => sesion.idS))];
                const salasData = await Promise.all(salaIds.map(async (idS) => {
                    const salaResponse = await axios.get(`Sala/GetById/${idS}`);
                    return { idS, ...salaResponse.data };
                }));
                setSalas(salasData);
            } catch (error) {
                console.error('Error al obtener las sesiones:', error);
            }
        };
        fetchScreenings();
    }, [movieId]);

    useEffect(() => {
        const fetchButacas = async () => {
            try {
                const butacasResponse = await axios.get('/api/Butaca/GetAll');
                const butacasDisponibles = butacasResponse.data.filter(butaca => butaca.IdS.toString() === selectedSala && butaca.Disponible);
                setAvailableSeats(butacasDisponibles.length);
            } catch (error) {
                console.error('Error al obtener las butacas:', error);
            }
        };

        if (selectedSala) fetchButacas();
    }, [selectedSala, setAvailableSeats]);

    const handleSalaChange = (e) => {
        setSelectedSala(e.target.value);
        setScreeningId('');
    };

    const handleSesionChange = (e) => {
        setScreeningId(e.target.value);
    };

    return (
        <div className="flex flex-col gap-3">
            <select className="select select-ghost select-sm bg-white/50 w-full max-w-xs" onChange={handleSalaChange} value={selectedSala}>
                <option disabled value="">Sala</option>
                {salas.map(sala => (
                    <option key={sala.IdS} value={sala.IdS}>Sala {sala.IdS} - Capacidad: {sala.Capacidad}</option>
                ))}
            </select>

            <select className={selectedSala ? 'select select-ghost select-sm bg-white/50 w-full max-w-xs visible' : 'select bg-white/50 w-full max-w-xs invisible'} onChange={handleSesionChange}>
                <option disabled value="">Horario</option>
                {screenings.filter(sesion => sesion.idS.toString() === selectedSala).map((sesion, index) => (
                    <option key={index} value={sesion.idP}>
                        {new Date(sesion.fecha).toLocaleDateString()} - {new Date(sesion.fecha).toLocaleTimeString()}
                    </option>
                ))}
            </select>
        </div>
    );
}

export default MovieScreeningSelect;



// import { useEffect, useState } from "react";

// const MovieScreeningSelect = ({ screenings, setScreeningId, defaultScreening }) => {

//     const getHorariosEnSala = (sala) => {
//         // Traigo todos los horarios de cada sala y los ordeno
//         const horariosSala = screenings.filter(s => s.sala == sala).map(s => {
//             return {
//                 time: s.horario.toDate(),
//                 id: s.id
//             }
//         });
//         horariosSala.sort((h1, h2) => h1.time - h2.time)

//         // Saco los días distintos que hay
//         const dias = [...new Set(horariosSala.map(h => h.time.toLocaleDateString()))];

//         // Recorro por día y acumulo los horarios del día en aux (con su ID).
//         const aux = []
//         for (const dia of dias) {
//             const horariosDelDia = horariosSala.filter(h => h.time.toLocaleDateString() == dia).map(h => {
//                 return {
//                     hora: h.time.getHours() + ':' + h.time.getMinutes(),
//                     id: h.id
//                 }
//             });
//             const funcion = { dia: dia, horarios: horariosDelDia }
//             aux.push(funcion);
//         }

//         return aux;
//     }

//     const salas = [...new Set(screenings.map(s => `${s.sala} - ${s.tipo} (${s.lenguaje})`))];

//     /* Esto es para habilitar al segundo select cuando se activo el primero y que este tenga los horarios de ESA sala */
//     const [funciones, setFunciones] = useState([]);

//     useEffect(() => {
//         // if si pasan de parámetro el id
//         if (defaultScreening) {
//             setFirstValue(`${defaultScreening.sala} - ${defaultScreening.tipo} (${defaultScreening.lenguaje})`)
//             setFunciones(getHorariosEnSala(defaultScreening.sala));

//             setSecondValue(defaultScreening.id)
//         }

//     }, [defaultScreening])

//     const [firstValue, setFirstValue] = useState('default');
//     const firstHandler = (e) => {
//         setFirstValue(e.target.value);

//         // Encuentro los horarios para esa sala
//         const sala = e.target.value.slice(0, 6);
//         setFunciones(getHorariosEnSala(sala));
//         setSecondValue('default');
//         setScreeningId(undefined);
//     }

//     const [secondValue, setSecondValue] = useState('default');
//     const secondHandler = (e) => {
//         setSecondValue(e.target.value);
//         setScreeningId(e.target.value);
//     }

//     return (
//         <div className="flex flex-col gap-3">
//             <select className="select select-ghost select-sm bg-white/50 w-full max-w-xs" value={firstValue} onChange={firstHandler}>
//                 <option disabled value={'default'} readOnly>Sala</option>
//                 {salas.map((sala, index) => (
//                     <option key={index} value={sala}> {sala} </option>
//                 ))}
//             </select>

//             <select className={funciones.length > 0 ? 'select select-ghost select-sm bg-white/50 w-full max-w-xs visible' : 'select bg-white/50 w-full max-w-xs invisible'} value={secondValue} onChange={secondHandler}>

//                 <option disabled value={'default'} readOnly>Horario</option>
//                 {funciones.map((func, i) => (
//                     <optgroup label={func.dia} key={i}>
//                         {func.horarios.map((horario, j) => (
//                             <option key={j} value={horario.id}> {`${func.dia} ${horario.hora}`} </option>
//                         ))}
//                     </optgroup>

//                 ))}

//             </select>

//         </div>
//     )
// }

// export default MovieScreeningSelect;