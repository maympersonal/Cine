// import React, { useEffect, useState } from "react";
// import { useParams } from "react-router-dom";
// import axios from '../../api/axios';
// import Loader from "../Loader/Loader";
// import MovieCardList from './MovieCardList';
// import { scrollTo } from '../Utils/functions';

// const MovieCardListContainer = () => {
//     const { categoryId = 'inicio' } = useParams();
//     const [loading, setLoading] = useState(true);
//     const [movieLists, setMovieLists] = useState([]);
//     const [listTitles, setListTitles] = useState([]);
//     const [genre, setGenre] = useState('');


//     useEffect(() => {
//         categoryId !== 'inicio' ? scrollTo('main') : scrollTo('body');
//         setLoading(true);

//         getMovies()
//             .then(res => {
//                 setMovieLists(res);
//                 setLoading(false);
//             })
//             .catch(err => console.log(err));

//         if (!isNaN(parseInt(categoryId))) {
//             // Aquí cambia la URL para adaptarse a tu backend .NET
//             axios.get(`/movies/genres`)
//                 .then(res => {
//                     setGenre(res.data.find(g => g.id === parseInt(categoryId)).name);
//                 })
//                 .catch(error => console.log(error));
//         }
//     }, [categoryId]);

//     useEffect(() => {
//         genre && setListTitles([`Cartelera - ${genre}`, `Próximos estrenos - ${genre}`]);
//     }, [genre]);

//     const getMovies = async () => {
//         let urls = [];

//         switch (categoryId) {
//             case 'inicio':
//                 setListTitles(['Cartelera', 'Próximos estrenos']);
//                 urls.push(`/movies/now_playing`);
//                 urls.push(`/movies/upcoming`);
//                 break;
//             case 'cartelera':
//                 setListTitles(['Cartelera']);
//                 urls.push(`/movies/now_playing`);
//                 break;
//             case 'estrenos':
//                 setListTitles(['Próximos estrenos']);
//                 urls.push(`/movies/upcoming`);
//                 break;
//             default:
//                 urls.push(`/movies/now_playing?genre=${categoryId}`);
//                 urls.push(`/movies/upcoming?genre=${categoryId}`);
//                 break;
//         }

//         try {
//             const data = await Promise.all(
//                 urls.map(url => axios.get(url).then(res => res.data))
//             );
//             return data;
//         } catch (err) {
//             console.error("Error fetching movies", err);
//             throw err;
//         }
//     };

//     return (
//         <div>
//             {!loading ? (
//                 movieLists.map((movieList, index) => (
//                     <MovieCardList movies={movieList} listTitle={listTitles[index]} key={`${categoryId}-${index}`} />
//                 ))
//             ) : <Loader />}
//         </div>
//     );
// }

// export default MovieCardListContainer;


import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Loader from "../Loader/Loader";
import MovieCardList from './MovieCardList';
import { scrollTo } from '../Utils/functions';

// Datos simulados para las listas de películas
const mockMovieLists = [
  {
    title: "Cartelera",
    movies: [
      {
        id: 1,
        title: "Película Simulada 1",
        poster_path: "/ruta-a-imagen1.jpg",
        // Otros campos necesarios
      },
      {
        id: 2,
        title: "Película Simulada 2",
        poster_path: "/ruta-a-imagen2.jpg",
        // Otros campos necesarios
      },
    ],
  },
  {
    title: "Próximos Estrenos",
    movies: [
      {
        id: 3,
        title: "Película Simulada 3",
        poster_path: "/ruta-a-imagen3.jpg",
        // Otros campos necesarios
      },
      {
        id: 4,
        title: "Película Simulada 4",
        poster_path: "/ruta-a-imagen4.jpg",
        // Otros campos necesarios
      },
    ],
  },
];

const MovieCardListContainer = () => {
    const categoryId = useParams().categoryId || 'inicio';
    const [loading, setLoading] = useState(false);
    const [movieLists, setMovieLists] = useState([]);
    const [listTitles, setListTitles] = useState([]);

    useEffect(() => {
        categoryId !== 'inicio' && scrollTo('main');
        setLoading(true);

        // Simulamos la carga de datos
        setTimeout(() => {
            setMovieLists(mockMovieLists.map(list => list.movies)); // Usamos solo las películas de los datos simulados
            setListTitles(mockMovieLists.map(list => list.title)); // Usamos los títulos de los datos simulados
            setLoading(false);
        }, 1000);
    }, [categoryId]);

    return (
        <div>
            {!loading ? (
                movieLists.map((movieList, index) => (
                    <MovieCardList movies={movieList} listTitle={listTitles[index]} key={categoryId + index} />
                ))
            ) : <Loader />}
        </div>
    );
};

export default MovieCardListContainer;
