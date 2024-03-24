// import axios from '../../api/axios';
// import { useState, useEffect } from "react";
// import { Link } from "react-router-dom";
// import { RemoveDuplicates } from '../Utils/functions';

// const SearchDropdown = ({ searchTerm, close }) => {
//     const [loading, setLoading] = useState(true);
//     const [movies, setMovies] = useState([]);
//     const [filteredMovies, setFilteredMovies] = useState([]);

//     // Ajusta esta URL base a la dirección de tu API


//     useEffect(() => {
//         if (searchTerm) {
//             setLoading(true);
//             axios.get(`/movies/search?query=${searchTerm}`)
//                 .then(response => {
//                     // Asumiendo que tu API devuelve los resultados en response.data.results
//                     const moviesWithoutDups = RemoveDuplicates(response.data, 'id');
//                     setMovies(moviesWithoutDups);
//                     setLoading(false);
//                 })
//                 .catch(error => {
//                     console.log(error);
//                     setLoading(false);
//                 });
//         }
//     }, [searchTerm]);

//     useEffect(() => {
//         if (movies.length > 0) {
//             const filteredMovies = movies.filter(m => m.title.toUpperCase().includes(searchTerm.toUpperCase()) ||
//                 m.original_title.toUpperCase().includes(searchTerm.toUpperCase()));
//             setFilteredMovies(filteredMovies);
//         } else {
//             setFilteredMovies([]);
//         }
//     }, [movies, searchTerm]);

//     return (
//         <div className="search-dropdown">
//             <ul className="dropdown-content menu w-full mt-2 font-albert">
//                 {!loading ?
//                     filteredMovies.length !== 0 ? filteredMovies.map(m => (
//                         <li key={m.id}>
//                             <Link to={`/movie/${m.id}`} onClick={close}>{m.title}</Link>
//                         </li>
//                     )) :
//                         <li className="btn rounded-none text-black bg-white pointer-events-none">Sin resultados</li>
//                     :
//                     <button className="btn rounded-none text-black bg-white loading pointer-events-none"></button>
//                 }
//             </ul>
//         </div>
//     );
// }

// export default SearchDropdown;


import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

// Datos simulados para las películas
const mockMovies = [
  { id: 1, title: 'El Rey León', },
  { id: 2, title: 'Buscando a Nemo', },
  { id: 3, title: 'Toy Story', },
  { id: 4, title: 'Los Increíbles', },
  // Agrega más películas según sea necesario
];

const SearchDropdown = ({ searchTerm, close }) => {
  const [loading, setLoading] = useState(false);
  const [filteredMovies, setFilteredMovies] = useState([]);

  useEffect(() => {
    if (!searchTerm) {
      setFilteredMovies([]);
      return;
    }

    // Simula una carga de datos
    setLoading(true);

    // Filtra los datos simulados basados en el término de búsqueda
    const results = mockMovies.filter(movie =>
      movie.title.toLowerCase().includes(searchTerm.toLowerCase())
    );

    setFilteredMovies(results);
    setLoading(false);
  }, [searchTerm]);

  return (
    <div className="search-dropdown">
      <ul className="dropdown-content menu w-full mt-2">
        {!loading ? (
          filteredMovies.length > 0 ? (
            filteredMovies.map(movie => (
              <li key={movie.id}>
                <Link to={`/movie/${movie.id}`} onClick={close}>{movie.title}</Link>
              </li>
            ))
          ) : (
            <li>Sin resultados</li>
          )
        ) : (
          <li>Cargando...</li>
        )}
      </ul>
    </div>
  );
};

export default SearchDropdown;
