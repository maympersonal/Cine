import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from '../../api/axios';

const GenresDropdown = () => {
  const [genres, setGenres] = useState([]);

  useEffect(() => {
    // La API /api/Genero/GetAll devuelve todos los géneros disponibles
    const fetchGenres = async () => {
      try {
        const response = await axios.get('Genero/GetAll');
        setGenres(response.data);
      } catch (error) {
        console.error('Error al obtener géneros:', error);
      }
    };

    fetchGenres();
  }, []);

  return (
    <div className="dropdown dropdown-hover">
      <label tabIndex={0} className="btn m-1">
        Géneros
      </label>
      <ul tabIndex={0} className="dropdown-content menu p-2 shadow bg-base-100 rounded-box w-52">
        {genres.map((genre) => (
          <li key={genre.idG}>
            <Link to={`/genre/${genre.idG}`}>{genre.nombreG}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default GenresDropdown;



// import { NavLink } from "react-router-dom";

// const GenerosDropdown = ({ genres, close }) => {
//     return (
//         <ul className="menu dropdown-content p-2 bg-white text-black ">
//             {genres.length > 0 ? (
//                 genres.map(g =>
//                     <li key={g.id} onClick={close}>
//                         <NavLink to={`./category/${g.id}`}>{g.name}</NavLink>
//                     </li>
//                 )
//             ) : (
//                 <button className="btn rounded-none text-black bg-white loading pointer-events-none"></button>
//             )}
//         </ul>
//     )
// }

// export default GenerosDropdown;