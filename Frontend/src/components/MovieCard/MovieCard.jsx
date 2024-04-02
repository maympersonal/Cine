// Asegúrate de importar axios desde tu configuración de axios personalizada
import axios from '../../api/axios';
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import movieNotFound from '/assets/img/movie-not-found.svg';

const MovieCard = ({ id }) => {
    const [movieDetails, setMovieDetails] = useState({});
    const [genres, setGenres] = useState([]);
    const [actors, setActors] = useState([]);

    const fetchMovieDetails = async () => {
        try {
            const { data } = await axios.get(`/api/Pelicula/GetById/${id}`);
            setMovieDetails(data);
            setGenres(data.idGs); // Directamente asigna los géneros ya que vienen como strings
            setActors(data.idAs); // Directamente asigna los actores ya que vienen como strings
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
                <img src={imgUrl} alt={`Poster of the movie '${movieDetails.titulo}'`} className={!movieDetails.imagen ? 'movieCard_img_notFound' : ''} />
            </div>
            <div className="movieCard_info">
                <h2 className="movieCard_info__title uppercase">{movieDetails.titulo}</h2>
                <p>{movieDetails.sinopsis || 'No synopsis available.'}</p>
                <div>Duration: {movieDetails.duración} minutes</div>
                <div>Genre(s): {genres.join(', ')}</div>
                <div>Cast: {actors.join(', ')}</div>
                <Link to={`/movie/${id}`}>
                    <button className="btn btn-sm btn-primary rounded uppercase movieCard_extraInfo_verFicha">View Full Details</button>
                </Link>
            </div>
        </div>
    );
};

export default MovieCard;
