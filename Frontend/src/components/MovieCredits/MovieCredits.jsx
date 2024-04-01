import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from '../../api/axios';
import Loader from '../Loader/Loader';
import { scrollTo } from '../Utils/functions';


const MovieCast = ({ name }) => {
    return (
        <div className="movieCast-name">
            <span>{name}</span>
        </div>
    );
};

const MovieCredits = () => {
    const [loading, setLoading] = useState(true);
    const { movieId } = useParams();
    const [cast, setCast] = useState([]);

    useEffect(() => {
        const fetchActorNames = async (actorIds) => {
            const actorNames = await Promise.all(actorIds.map(async (idA) => {
                const { data } = await axios.get(`Actor/GetById/${idA}`);
                return data.nombreA;
            }));
            return actorNames;
        };

        const getCredits = async () => {
            setLoading(true);
            try {
                const { data: pelicula } = await axios.get(`Pelicula/GetById/${movieId}`);
                if (pelicula) {
                    const actorNames = await fetchActorNames(pelicula.idAs);
                    setCast(actorNames.map(name => ({ name })));
                }
            } catch (error) {
                console.error("Error fetching movie credits:", error);
            } finally {
                setLoading(false);
            }
        };

        scrollTo('main');
        getCredits();
    }, [movieId]);

    return (
        <div className="movieCredits flex flex-col items-center mb-7 pb-10">
            <h2 className="text text-4xl uppercase mb-14 underline">Créditos</h2>
            {!loading ? (
                cast.length > 0 ? (
                    <ul>
                        {cast.map((actor, index) => (
                            <li key={index}>
                                <MovieCast name={actor.name} />
                            </li>
                        ))}
                    </ul>
                ) : <p>No se encontraron créditos para esta película.</p>
            ) : <Loader />
            }
        </div>
    );
};

export default MovieCredits;





// import React, { useEffect, useState } from "react";
// import { useParams } from "react-router-dom";
// import axios from '../../api/axios'; // Importamos axios
// import Loader from '../Loader/Loader';
// import { scrollTo } from '../Utils/functions';
// import MovieCast from "./MovieCast";
// import MovieCrew from "./MovieCrew";

// const mockCreditsData = {
// //     cast: [
// //       { credit_id: "cast1", character: "John Doe", name: "Actor 1", profile_path: "/path-to-image-1.jpg" },
// //       { credit_id: "cast2", character: "Jane Smith", name: "Actriz 2", profile_path: "/path-to-image-2.jpg" },
// //       // Agrega más miembros del reparto si es necesario
// //     ],
// //     crew: [
// //       { credit_id: "crew1", job: "Director", name: "Director 1", profile_path: "/path-to-image-3.jpg" },
// //       { credit_id: "crew2", job: "Producer", name: "Productor 2", profile_path: "/path-to-image-4.jpg" },
// //       // Agrega más miembros del equipo si es necesario
// //     ],
// //   };

//   const MovieCredits = () => {
//     const [loading, setLoading] = useState(true);
//     const { movieId } = useParams();
//     const [credits, setCredits] = useState({ cast: [], crew: [] });

//     useEffect(() => {
//       // Aquí simularías la carga de datos, por ejemplo con un delay
//       setLoading(true);
//       setTimeout(() => {
//         setCredits(mockCreditsData);
//         setLoading(false);
//       }, 1000);
//     }, [movieId]);

// const MovieCredits = () => {
//     const [loading, setLoading] = useState(true);
//     const { movieId } = useParams();
//     const [credits, setCredits] = useState({ cast: [], crew: [] });

//     useEffect(() => {
//         const getCredits = async () => {
//             const url = `/movies/${movieId}/credits`;
//             try {
//                 const response = await axios.get(url);
//                 setCredits(response.data);
//             } catch (error) {
//                 console.error("Error fetching movie credits from backend:", error);
//             } finally {
//                 setLoading(false);
//             }
//         };

//         scrollTo('main');
//         getCredits();
//     }, [movieId]); // Dependencia [movieId] para reaccionar a cambios en el ID de la película

//     return (
//         <div className="movieCredits flex flex-col items-center mb-7 pb-10">
//             <h2 className="text text-4xl uppercase mb-14 underline font-bowlby">Créditos</h2>
//             {!loading ? (
//                 <div className="flex justify-center bg-white/80 text-black p-5">
//                     {/* Renderizado condicional basado en la carga de los datos */}
//                     <ul className="w-1/2">
//                         {/* Detalles del reparto */}
//                         <div className="mb-10 flex items-center text-xs sm:text-2xl gap-2">
//                             <span className="font-bold">Reparto</span>
//                             <span className="text-slate-600">{credits.cast.length}</span>
//                         </div>
//                         {credits.cast.map(c => (
//                             <li key={c.credit_id}>
//                                 <MovieCast {...c} />
//                             </li>
//                         ))}
//                     </ul>
//                     <ul className="w-1/2">
//                         {/* Detalles del equipo */}
//                         <div className="mb-10 flex items-center text-xs sm:text-2xl gap-2">
//                             <span className="font-bold">Equipo</span>
//                             <span className="text-slate-600">{credits.crew.length}</span>
//                         </div>
//                         {credits.crew.map(c => (
//                             <li key={c.credit_id}>
//                                 <MovieCrew {...c} />
//                             </li>
//                         ))}
//                     </ul>
//                 </div>
//             ) : (
//                 <Loader />
//             )}
//         </div>
//     );
// }

// export default MovieCredits;
