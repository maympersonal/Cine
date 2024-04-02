//

import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from '../../api/axios';
import Loader from '../Loader/Loader';
import MovieDetail from './MovieDetail';
import { scrollTo } from '../Utils/functions';

const MovieDetailContainer = () => {
    const [loading, setLoading] = useState(false);
    const { movieId } = useParams();
    const [movie, setMovie] = useState();


    useEffect(() => {
        scrollTo('main');
        setLoading(true);


        axios.get(`/Pelicula/${movieId}`)
            .then(res => {
                setMovie(res.data);
                setLoading(false);
            })
            .catch((err) => {
                console.log(err);
                setLoading(false);
            });
    }, [movieId]);

    return (
       <div>
            {!loading ? (movie && <MovieDetail {...movie} />) : <Loader />}
       </div>
    );
}

// export default MovieDetailContainer;

// import React, { useEffect, useState } from "react";
// import { useParams } from "react-router-dom";
// import Loader from '../Loader/Loader';
// import MovieDetail from './MovieDetail';
// import { scrollTo } from '../Utils/functions';

// // Datos simulados para el detalle de una película
// const mockMovieData = {
//   id: "movie123",
//   title: "Película Simulada",
//   overview: "Esta es una descripción simulada de la película.",
//   poster_path: "/ruta-a-imagen.jpg",
//   genres: [{ id: 1, name: "Acción" }, { id: 2, name: "Aventura" }],
//   // Otros campos que tu componente MovieDetail pueda necesitar
// };

// const MovieDetailContainer = () => {
//   const [loading, setLoading] = useState(false);
//   const { movieId } = useParams();
//   const [movie, setMovie] = useState();

//   useEffect(() => {
//     // Simulamos una llamada a la API
//     const fetchMovie = async () => {
//       setLoading(true);
//       // Aquí realizarías la llamada a la API real.
//       // Simulamos un retraso de la red con setTimeout
//       setTimeout(() => {
//         setMovie(mockMovieData); // Establecemos los datos simulados
//         setLoading(false);
//       }, 1000);
//     };

//     scrollTo('main');
//     fetchMovie();
//   }, [movieId]);

//   return (
//     <div>
//       {!loading ? movie && <MovieDetail {...movie}/> : <Loader/>}
//     </div>
//   )
// }

// export default MovieDetailContainer;
