import axios from '../../api/axios';
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import movieNotFound from '/assets/img/movie-not-found.svg';

const MovieCard = ({ id }) => {
    const [movieDetails, setMovieDetails] = useState({});
    const [genres, setGenres] = useState([]);
    const [actors, setActors] = useState([]);
    const [esEstreno, setEsEstreno] = useState(false);

    useEffect(() => {
        // Aquí asumimos que la lógica para determinar si es un estreno
        // se basa en alguna propiedad de movieDetails como la fecha de estreno
    }, [movieDetails]);

    useEffect(() => {
        const fetchMovieDetails = async () => {
            try {
                // Supongamos que tu API ya te devuelve toda la información necesaria en un solo llamado
                const { data } = await axios.get(`/api/Pelicula/GetById/${id}`);
                setMovieDetails(data);

                // Luego, obtienes los géneros y actores usando los IDs
                // Esta parte asume que tienes end-points separados para géneros y actores
                const fetchedGenres = await Promise.all(data.idGs.map(async (genreId) => {
                    const response = await axios.get(`/api/Genero/GetById/${genreId}`);
                    return response.data;
                }));
                setGenres(fetchedGenres);

                const fetchedActors = await Promise.all(data.idAs.map(async (actorId) => {
                    const response = await axios.get(`/api/Actor/GetById/${actorId}`);
                    return response.data;
                }));
                setActors(fetchedActors);

            } catch (error) {
                console.error('Error fetching movie details:', error);
            }
        };

        fetchMovieDetails();
    }, [id]);

    const imgUrl = movieDetails.imagen || movieNotFound;

    return (
        <div className="movieCard">
            <div className="movieCard_img">
                <img src={imgUrl} alt={`Poster of the movie '${movieDetails.titulo}'`} />
            </div>
            <div className="movieCard_info">
                {esEstreno &&
                    <div className="movieCard_info__premiere">ESTRENO</div>
                }
                <h2 className="movieCard_info__title">{movieDetails.titulo}</h2>
                <p className="movieCard_info__overview">{movieDetails.sinopsis}</p>
                <div className="movieCard_info__meta">
                    <span>Duración: {movieDetails.duración} min</span>
                    <span>Género(s): {genres.map(genre => genre.nombreG).join(', ')}</span>
                    <span>Cast: {actors.map(actor => actor.nombreA).join(', ')}</span>
                </div>
                <Link to={`/movie/${id}`} className="movieCard_info__more">
                    Ver más
                </Link>
            </div>
        </div>
    );
};

export default MovieCard;




// import axios from '../../api/axios';
// import { useEffect, useState } from "react";
// import { Link } from "react-router-dom";
// import movieNotFound from '/assets/img/movie-not-found.svg';

// const MovieCard = ({ id }) => {
//     // Estados para almacenar los detalles de la película, géneros y actores
//     const [movieDetails, setMovieDetails] = useState({});
//     const [genres, setGenres] = useState([]);
//     const [actors, setActors] = useState([]);


//     const fetchMovieDetails = async () => {
//         try {
//             const { data } = await axios.get(`Pelicula/GetAll`);
//             setMovieDetails(data);


//             const genresData = await Promise.all(data.idGs.map(async (genreId) => {
//                 const response = await axios.get(`Genero/GetById/${genreId}`);
//                 return response.data;
//             }));
//             setGenres(genresData);


//             const actorsData = await Promise.all(data.idAs.map(async (actorId) => {
//                 const response = await axios.get(`Actor/GetById/${actorId}`);
//                 return response.data;
//             }));
//             setActors(actorsData);
//         } catch (error) {
//             console.error('Error fetching movie details:', error);
//         }
//     };

//     useEffect(() => {
//         fetchMovieDetails();
//     }, [id]);

//     const imgUrl = movieDetails.imagen ? movieDetails.imagen : movieNotFound;

//     return (
//         <div className="movieCard">
//             <div className="movieCard_img">
//                 <img src={imgUrl} alt={`Poster de la película '${movieDetails.titulo}'`} />
//             </div>
//             <div className="movieCard_info">
//                 <h2 className="movieCard_info__title">{movieDetails.titulo}</h2>
//                 <p>{movieDetails.sinopsis}</p>
//                 <div>Duración: {movieDetails.duración} minutos</div>
//                 <div>Géneros: {genres.map(genre => genre.nombreG).join(', ')}</div>
//                 <div>Actores: {actors.map(actor => actor.nombreA).join(', ')}</div>
//                 <Link to={`/movie/${id}`}>
//                     <button className="btn">Ver detalles completos</button>
//                 </Link>
//             </div>
//         </div>
//     );
// };

// export default MovieCard;



// import axios from '../../api/axios';
// import { useEffect, useState } from "react";
// import { Link } from "react-router-dom";
// import movieNotFound from '/assets/img/movie-not-found.svg';

// const MovieCard = ({ id }) => {
//     const [movieDetails, setMovieDetails] = useState({});
//     const [genres, setGenres] = useState([]);
//     const [actors, setActors] = useState([]);

//     const fetchMovieDetails = async () => {
        // try {
        //     const { data } = await axios.get(`Pelicula/GetAll`);
        //     setMovieDetails(data);


        //     const genresData = await Promise.all(data.idGs.map(async (genreId) => {
        //         const response = await axios.get(`Genero/GetById/${genreId}`);
        //         return response.data;
        //     }));
        //     setGenres(genresData);


        //     const actorsData = await Promise.all(data.idAs.map(async (actorId) => {
        //         const response = await axios.get(`Actor/GetById/${actorId}`);
        //         return response.data;
        //     }));
        //     setActors(actorsData);
        // } catch (error) {
        //     console.error('Error fetching movie details:', error);
        // }
//     };

//     useEffect(() => {
//         fetchMovieDetails();
//     }, [id]);

//     const imgUrl = movieDetails.imagen ? movieDetails.imagen : movieNotFound;

//     return (
//         <div className="movieCard">
//             <div className="movieCard_img">
//                 <img src={imgUrl} alt={`Poster of the movie '${movieDetails.titulo}'`} className={!movieDetails.imagen ? 'movieCard_img_notFound' : ''} />
//             </div>
//             <div className="movieCard_info">
//                 <h2 className="movieCard_info__title uppercase">{movieDetails.titulo}</h2>
//                 <p>{movieDetails.sinopsis || 'No synopsis available.'}</p>
//                 <div>
//                     Duration: {movieDetails.duración} minutes
//                 </div>
//                 <div>
//                     Genre(s): {genres.map(genre => genre.nombreG).join(', ')}
//                 </div>
//                 <div>
//                     Cast: {actors.map(actor => actor.nombreA).join(', ')}
//                 </div>
//                 <Link to={`/movie/${id}`}>
//                     <button className="btn btn-sm btn-primary rounded uppercase movieCard_extraInfo_verFicha">View Full Details</button>
//                 </Link>
//             </div>
//         </div>
//     );
// };

// export default MovieCard;
