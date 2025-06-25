import styles from './Home.module.css'
import {coins} from "./coins.data.js";

function Home() {

    return (
        <div>
            <h1>Treasure Collector</h1>
            <div>
                {coins.map(coin => (
                    <div key={coin.id} className={styles.item}>
                        <div className={styles.image}
                             style={{
                                 backgroundImage: `url(${coin.image})`,
                             }}
                        ></div>
                        <div className={styles.info}>
                            <h2>{coin.name}</h2>
                            <p>ID {coin.id}</p>
                            <button>View</button>
                        </div>
                    </div>

                )) }
            </div>

        </div>

    )
}

export default Home