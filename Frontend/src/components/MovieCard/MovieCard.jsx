import { Link } from "react-router-dom";
import movieNotFound from '/assets/img/movie-not-found.svg';


const MovieCard = ({
    idP,
    titulo,
    sinopsis,
    imagen,
    duración,
    idAs,
    idGs
}) => {

    const imgUrl = imagen ? imagen : movieNotFound;


    const genres = idGs.join(', ');
    const actors = idAs.join(', ');

    return (
        <div className="movieCard">
            <div className="movieCard_img">
                <img src={imgUrl} alt={`Poster de la película '${titulo}'`} className={!imagen ? 'movieCard_img_notFound' : ''} />
            </div>
            <div className="movieCard_info">
                <h2 className="movieCard_info__title uppercase">{titulo}</h2>
                <p>{sinopsis || 'No hay información sobre la sinopsis de esta película.'}</p>
                <div>Duración: {duración} minutos</div>
                <div>Género(s): {genres}</div>
                <div>Elenco: {actors}</div>
                <Link to={`/movie/${idP}`}>
                    <button className="btn btn-sm btn-primary rounded uppercase movieCard_extraInfo_verFicha">Ver ficha completa</button>
                </Link>
            </div>
        </div>
    );
};

export default MovieCard;
