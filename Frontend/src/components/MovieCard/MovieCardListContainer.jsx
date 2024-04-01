import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from '../../api/axios';
import Loader from "../Loader/Loader";
import MovieCardList from './MovieCardList';
import { scrollTo } from '../Utils/functions';

const MovieCardListContainer = () => {
    const { categoryId = 'inicio' } = useParams();
    const [loading, setLoading] = useState(true);
    const [movies, setMovies] = useState([]);
    const [listTitles, setListTitles] = useState([]);
    const [genre, setGenre] = useState('');

    useEffect(() => {
        categoryId !== 'inicio' ? scrollTo('main') : scrollTo('body');
        setLoading(true);
        const fetchMovies = async () => {
            try {
                const response = await axios.get('Pelicula/GetAll');
                const fetchedMovies = response.data;
                const moviesWithDetails = await Promise.all(fetchedMovies.map(async movie => {

                    const genres = await Promise.all(movie.idGs.map(async idG => {
                        const genreResponse = await axios.get(`Genero/GetById/${idG}`);
                        return genreResponse.data.nombreG;
                    }));

                    const actors = await Promise.all(movie.idAs.map(async idA => {
                        const actorResponse = await axios.get(`Actor/GetById/${idA}`);
                        return actorResponse.data.nombreA;
                    }));
                    return { ...movie, genres, actors };
                }));
                setMovies(moviesWithDetails);
            } catch (error) {
                console.error('Error fetching movies:', error);
            }
            setLoading(false);
        };
        fetchMovies();
    }, [categoryId]);

    useEffect(() => {
        if (!isNaN(parseInt(categoryId))) {
            axios.get(`Genero/GetById/${categoryId}`)
                .then(res => {
                    setGenre(res.data.nombreG);
                    setListTitles([`Cartelera - ${res.data.nombreG}`, `Próximos estrenos - ${res.data.nombreG}`]);
                })
                .catch(error => console.error('Error fetching genre name:', error));
        } else {
            setListTitles(['Cartelera', 'Próximos estrenos']);
        }
    }, [genre, categoryId]);

    return (
        <div>
            {!loading ? (
                movies.length > 0 ? (
                    movies.map((movie, index) => (
                        <MovieCardList key={index} movies={[movie]} listTitle={listTitles[index % listTitles.length]} />
                    ))
                ) : (
                    <div>No se encontraron películas.</div>
                )
            ) : (
                <Loader />
            )}
        </div>
    );
};

export default MovieCardListContainer;



// import React, { useEffect, useState } from "react";
// import { useParams } from "react-router-dom";
// import Loader from "../Loader/Loader";
// import MovieCardList from './MovieCardList';
// import { scrollTo } from '../Utils/functions';

// // Datos simulados para las listas de películas
// const mockMovieLists = [
//   {
//     title: "Cartelera",
//     movies: [
//       {
//         id: 1,
//         title: "Película Simulada 1",
//         poster_path: "/ruta-a-imagen1.jpg",
//         // Otros campos necesarios
//       },
//       {
//         id: 2,
//         title: "Película Simulada 2",
//         poster_path: "/ruta-a-imagen2.jpg",
//         // Otros campos necesarios
//       },
//     ],
//   },
//   {
//     title: "Próximos Estrenos",
//     movies: [
//       {
//         id: 3,
//         title: "Película Simulada 3",
//         poster_path: "/ruta-a-imagen3.jpg",
//         // Otros campos necesarios
//       },
//       {
//         id: 4,
//         title: "Película Simulada 4",
//         poster_path: "/ruta-a-imagen4.jpg",
//         // Otros campos necesarios
//       },
//     ],
//   },
// ];

// const MovieCardListContainer = () => {
//     const categoryId = useParams().categoryId || 'inicio';
//     const [loading, setLoading] = useState(false);
//     const [movieLists, setMovieLists] = useState([]);
//     const [listTitles, setListTitles] = useState([]);

//     useEffect(() => {
//         categoryId !== 'inicio' && scrollTo('main');
//         setLoading(true);

//         // Simulamos la carga de datos
//         setTimeout(() => {
//             setMovieLists(mockMovieLists.map(list => list.movies)); // Usamos solo las películas de los datos simulados
//             setListTitles(mockMovieLists.map(list => list.title)); // Usamos los títulos de los datos simulados
//             setLoading(false);
//         }, 1000);
//     }, [categoryId]);

//     return (
//         <div>
//             {!loading ? (
//                 movieLists.map((movieList, index) => (
//                     <MovieCardList movies={movieList} listTitle={listTitles[index]} key={categoryId + index} />
//                 ))
//             ) : <Loader />}
//         </div>
//     );
// };

// export default MovieCardListContainer;
