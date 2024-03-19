import popCorn from '/assets/img/popcorn.svg';

const Footer = () => {
  return (
    <footer className="footer footer-center p-10 text-primary-content">
        <div>
            <img src={popCorn} alt="Pochoclos Icono" width="100px"/>
            <p className="font-bold uppercase ">
                Cine++
            </p>
        </div>
    </footer>
  )
}
export default Footer