import axios from '../../api/axios';
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import movieNotFound from '/assets/img/movie-not-found.svg';

const MovieCard = ({ id }) => {
    // Estados para almacenar los detalles de la película, géneros y actores
    const [movieDetails, setMovieDetails] = useState({});
    const [genres, setGenres] = useState([]);
    const [actors, setActors] = useState([]);


    const fetchMovieDetails = async () => {
        try {
            const { data } = await axios.get(`Pelicula/GetAll`);
            setMovieDetails(data);


            const genresData = await Promise.all(data.idGs.map(async (genreId) => {
                const response = await axios.get(`Genero/GetById/${genreId}`);
                return response.data;
            }));
            setGenres(genresData);


            const actorsData = await Promise.all(data.idAs.map(async (actorId) => {
                const response = await axios.get(`Actor/GetById/${actorId}`);
                return response.data;
            }));
            setActors(actorsData);
        } catch (error) {
            console.error('Error fetching movie details:', error);
        }
    };

    useEffect(() => {
        fetchMovieDetails();
    }, [id]);

    const imgUrl = movieDetails.imagen ? movieDetails.imagen : movieNotFound;

    return (
        <div className="movieCard">
            <div className="movieCard_img">
                <img src={imgUrl} alt={`Poster de la película '${movieDetails.titulo}'`} />
            </div>
            <div className="movieCard_info">
                <h2 className="movieCard_info__title">{movieDetails.titulo}</h2>
                <p>{movieDetails.sinopsis}</p>
                <div>Duración: {movieDetails.duración} minutos</div>
                <div>Géneros: {genres.map(genre => genre.nombreG).join(', ')}</div>
                <div>Actores: {actors.map(actor => actor.nombreA).join(', ')}</div>
                <Link to={`/movie/${id}`}>
                    <button className="btn">Ver detalles completos</button>
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
