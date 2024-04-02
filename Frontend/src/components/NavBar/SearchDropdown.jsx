import axios from '../../api/axios';
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { RemoveDuplicates } from '../Utils/functions';

const SearchDropdown = ({ searchTerm, close }) => {
    const [loading, setLoading] = useState(true);
    const [movies, setMovies] = useState([]);
    const [filteredMovies, setFilteredMovies] = useState([]);

    // Ajusta esta URL base a la dirección de tu API


    useEffect(() => {
        if (searchTerm) {
            setLoading(true);
            axios.get(`/movies/search?query=${searchTerm}`)
                .then(response => {
                    // Asumiendo que tu API devuelve los resultados en response.data.results
                    const moviesWithoutDups = RemoveDuplicates(response.data, 'id');
                    setMovies(moviesWithoutDups);
                    setLoading(false);
                })
                .catch(error => {
                    console.log(error);
                    setLoading(false);
                });
        }
    }, [searchTerm]);

    useEffect(() => {
        if (movies.length > 0) {
            const filteredMovies = movies.filter(m => m.title.toUpperCase().includes(searchTerm.toUpperCase()) ||
                m.original_title.toUpperCase().includes(searchTerm.toUpperCase()));
            setFilteredMovies(filteredMovies);
        } else {
            setFilteredMovies([]);
        }
    }, [movies, searchTerm]);

    return (
        <div className="search-dropdown">
            <ul className="dropdown-content menu w-full mt-2 font-albert">
                {!loading ?
                    filteredMovies.length !== 0 ? filteredMovies.map(m => (
                        <li key={m.id}>
                            <Link to={`/movie/${m.id}`} onClick={close}>{m.title}</Link>
                        </li>
                    )) :
                        <li className="btn rounded-none text-black bg-white pointer-events-none">Sin resultados</li>
                    :
                    <button className="btn rounded-none text-black bg-white loading pointer-events-none"></button>
                }
            </ul>
        </div>
    );
}

export default SearchDropdown;

// import React from "react";
// import { Link } from "react-router-dom";

// const SearchDropdown = ({ searchResults }) => {
//     return (
//         <div className="absolute top-full left-0 right-0 z-10 bg-white shadow-lg max-h-60 overflow-auto">
//             {searchResults.length > 0 ? (
//                 searchResults.map((movie) => (
//                     <Link
//                         to={`/movie/${movie.id}`}
//                         className="block px-4 py-2 hover:bg-gray-100"
//                         key={movie.id}
//                     >
//                         {movie.titulo}
//                     </Link>
//                 ))
//             ) : (
//                 <div className="px-4 py-2">No se encontraron películas.</div>
//             )}
//         </div>
//     );
// };

// export default SearchDropdown;
