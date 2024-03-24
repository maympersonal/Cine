// Asegúrate de importar axios desde tu configuración de axios personalizada
import axios from '../../api/axios'; // Ajusta la ruta según corresponda
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import movieNotFound from '/assets/img/movie-not-found.svg'
// import PropTypes from 'prop-types';

// MovieCard.propTypes = {
//     title: PropTypes.string.isRequired,
//     overview: PropTypes.string.isRequired,
//     poster_path: PropTypes.string,
//     id: PropTypes.number.isRequired,
//     release_date: PropTypes.string.isRequired,
//     start: PropTypes.instanceOf(Date).isRequired,
//     end: PropTypes.instanceOf(Date).isRequired,
//     listTitle: PropTypes.string.isRequired,
// };



const MovieCard = ({ title, overview, poster_path, id, release_date, start, end, listTitle }) => {
    // Lógica para determinar si es un estreno
    const [esEstreno, setEstreno] = useState(false);
    useEffect(() => {
        let releaseDate = new Date(release_date + 'T00:00');
        releaseDate.setHours(0, 0, 0, 0);

        if (releaseDate.getTime() >= start.getTime() && releaseDate.getTime() < end.getTime()) {
            setEstreno(true);
        }
    }, [release_date, start, end])

    // Estado para los detalles de la película
    const [details, setDetails] = useState('');

    // Llamada al backend para obtener los detalles de la película
    useEffect(() => {
        axios.get(`/peliculas/${id}`)
            .then(res => {
                const data = res.data;
                setDetails({
                    runtime: data.runtime,
                    genres: data.genres,
                    director: data.director,
                    cast: data.cast,
                    rate: data.rate
                });
            })
            .catch(error => console.log(error));
    }, [id])

    const { runtime, genres, director, cast, rate } = details;
    const imgUrl = poster_path ? `https://image.tmdb.org/t/p/original/${poster_path}` : movieNotFound;

    // Renderización del componente ajustada a los nuevos datos
    return  (
        <div className="movieCard">
            <div className="movieCard_img">
                <img src={imgUrl} alt={`Afiche de la película '${title}'`} className={!poster_path ? 'movieCard_img_notFound' : ''} />
            </div>

            <div className="movieCard_info">

                {esEstreno && !listTitle.includes('Próximos estrenos') &&
                    <ul className="movieCard_info__premiere rounded font-bold uppercase ">
                        <li><i className="fa-solid fa-star"></i></li>
                        <li className="flex"><span className="text-center">Estreno de la semana</span></li>
                        <li><i className="fa-solid fa-star"></i></li>
                    </ul>
                }

                <h2 className="movieCard_info__title uppercase">{title}</h2>

                <div className="movieCard_info__review">

                    <p className="">{overview ? (overview.length <= 200 ? overview : overview.slice(0, 200) + '...') : 'No hay información sobre la sinopsis de esta película.'}</p>
                </div>

                <div>
                    <hr className="mb-1"></hr>
                    <ul className="movieCard_info__details uppercase">
                        <li className={details && runtime ? '' : "hidden"}>Duración: {`${details && runtime} min`}</li>
                        <li className=""> Género: {genres && genres[0] && genres[0].name} </li>
                    </ul>
                    <hr className="mt-1"></hr>
                </div>

            </div>


            <ul className="movieCard_extraInfo uppercase">

                <li className="movieCard_extraInfo_item">

                    <ul className="">
                        <li><i className="fa-solid fa-film"></i></li>
                        <li>{director && director}</li>
                    </ul>

                    <ul className="">
                        <li><i className="fa-solid fa-star"></i></li>
                        <li><p>{cast && cast.map(actor => actor).slice(0, 3).join(', ') + (cast.length > 4 && '...')}</p></li>
                    </ul>

                    <ul className="">
                        <li><i className="fa-solid fa-user-group"></i></li>
                        <li className="uppercase">{details.rate || 'Sin info'}</li>
                    </ul>
                </li>

                <Link to={`/movie/${id}`}>
                    <button className="btn btn-sm btn-primary rounded uppercase movieCard_extraInfo_verFicha">Ver ficha completa</button>
                </Link>


            </ul>

        </div>
    )
}

export default MovieCard;