import express from 'express';
import cors from 'cors';
import mysql from 'mysql2/promise';

const app = express();app.use(cors()); 
app.use(express.json());

const dbConfig = {
    host: 'localhost',
    user:'root',
    password: '',
    database: 'gyumolcsok',
    port: 3306
};
const connection = await mysql.createConnection(dbConfig);
app.get('/fruits', async (req, res) => {
    try {
        
        const [rows] = await connection.execute('SELECT * FROM gyumolcs');
        res.json(rows);
    } catch (error) {
        console.error('Error fetching fruits:', error);
        res.status(500).json({ error: 'Internal server error' });
    }
});
app.post('/fruits', async (req, res) => {
const { nev, megjegyzes, nev_eng, alt_szoveg, src } = req.body;
    try {
        const [result] = await connection.execute('INSERT INTO `gyumolcs` (`gyumolcsid`, `nev`, `megjegyzes`, `nev_eng`, `alt_szoveg`, `src`) VALUES ( NULL, ?, ?, ?, ?, ?)', [nev, megjegyzes, nev_eng, alt_szoveg, src]);
        res.status(201).json({ id: result.insertId, nev, megjegyzes, nev_eng, alt_szoveg, src });}
    catch (error) {
    console.error('Error adding fruit:', error);
    res.status(500).json({ error: 'Internal server error' });
}});
app.get('/fruits/:id', async (req, res) => {
    const { id } = req.params;
    try {
        const [rows] = await connection.execute('SELECT * FROM `gyumolcs` WHERE `gyumolcsid` = ?', [id]);
        if (rows.length === 0) {
            res.status(404).json({ error: 'Fruit not found' });
        } else {
            res.json(rows[0]);
        }
    } catch (error) {
        console.error('Error fetching fruit:', error); res.status(500).json({ error: 'Internal server error' })
    };
});
app.post('/fruits/:id', async (req, res) => {
    const { id } = req.params;
    const { nev, megjegyzes, nev_eng, alt_szoveg, src } = req.body;
    try {
        const [result] = await connection.execute('UPDATE `gyumolcs` SET `nev` = ?, `megjegyzes` = ?, `nev_eng` = ?, `alt_szoveg` = ?, `src` = ? WHERE `gyumolcsid` = ?', [nev, megjegyzes, nev_eng, alt_szoveg, src, id]);
        if (result.affectedRows === 0) {
            res.status(404).json({ error: 'Fruit not found' });
        } else {
            res.json({ message: 'Fruit updated successfully' });
        }
    } catch (error) {
        console.error('Error updating fruit:', error);
        res.status(500).json({ error: 'Internal server error' });
    }
});
//meglévő gyümölcs adatainak a frissítése.
app.put('/fruits/:id', async (req, res) => {
    const { id } = req.params;
    const { nev, megjegyzes, nev_eng, alt_szoveg, src } = req.body;
    try {
    const [result] = await connection.execute('UPDATE `gyumolcs` SET `nev` = ?, `megjegyzes` = ?, `nev_eng` = ?, `alt_szoveg` = ?, `src` = ? WHERE `gyumolcsid` = ?', [nev, megjegyzes, nev_eng, alt_szoveg, src, id]);
    if (result.affectedRows === 0) {
    res.status(404).json({ error: 'Fruit not found' });
    } else {
        res.json({ message: 'Fruit updated successfully' });
    }}
    catch (error) {
        console.error('Error updating fruit:', error); 
        res.status(500).json({ error: 'Internal server error' }); 
    }
});
app.delete('/fruits/:id', async (req, res) => {
    const { id } = req.params;
    try {
        const [result] = await connection.execute('DELETE FROM `gyumolcs` WHERE `gyumolcsid` = ?', [id]);
        if (result.affectedRows === 0) {
            res.status(404).json({ error: 'Fruit not found' });
        }
        else {
            res.json({ message: 'Fruit deleted successfully' }); }
        } catch (error) {
            console.error('Error deleting fruit:', error); 
            res.status(500).json({ error: 'Internal server error' }); 
        }
});  
app.listen(3000, () => {console.log('Server is running on http://localhost:3000'); });